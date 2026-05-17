/*
 * MainCanvasForm.cs
 * Core GUI controller for the Paint application.
 * This file coordinates drawing tools, canvas state, save operations, and history.
 */

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using DrawingTools;
using StateManager;

namespace PaintApp;

/// <summary>
/// Represents the main application window and coordinates user interaction with the canvas.
/// </summary>
public partial class MainCanvasForm : Form, ICanvasHost
{
    private readonly CommandManager _historyManager = new();
    private readonly Dictionary<ToolStripButton, IDrawStrategy> _toolBindings = new();

    private IDrawStrategy? _currentTool;
    private Bitmap? _currentImage;
    private Bitmap? _previewImage;
    private Bitmap? _drawingStartImage;
    private Point _startPoint;
    private Point _lastPoint;
    private bool _isDrawing;
    private Color _selectedColor = Color.Black;
    private float _selectedThickness = 2f;

    /// <summary>
    /// Initializes the main form and wires the application services.
    /// </summary>
    public MainCanvasForm()
    {
        InitializeComponent();
        ConfigureApplication();
    }

    /// <summary>
    /// Replaces the current image displayed on the canvas.
    /// </summary>
    /// <param name="image">The image that becomes the new canvas content.</param>
    /// <exception cref="ArgumentNullException">Thrown when the supplied bitmap is null.</exception>
    public void SetCanvasImage(Bitmap image)
    {
        ArgumentNullException.ThrowIfNull(image);

        _previewImage?.Dispose();
        _previewImage = null;

        _currentImage?.Dispose();
        _currentImage = image;

        canvasPanel.Invalidate();
    }

    /// <summary>
    /// Configures tools, event handlers, and the initial UI state.
    /// </summary>
    private void ConfigureApplication()
    {
        _historyManager.HistoryChanged += HistoryManager_HistoryChanged;

        RegisterTool(pencilToolStripButton, new PencilTool());
        RegisterTool(lineToolStripButton, new LineTool());
        RegisterTool(rectangleToolStripButton, new RectangleTool());
        RegisterTool(ellipseToolStripButton, new EllipseTool());

        saveToolStripButton.Click += SaveToolStripButton_Click;
        undoToolStripButton.Click += UndoToolStripButton_Click;
        redoToolStripButton.Click += RedoToolStripButton_Click;
        colorToolStripButton.Click += ColorToolStripButton_Click;
        thicknessToolStripComboBox.SelectedIndexChanged += ThicknessToolStripComboBox_SelectedIndexChanged;

        thicknessToolStripComboBox.SelectedIndex = 0;
        SelectTool(pencilToolStripButton);
        UpdateColorButton();
        UpdateHistoryButtons();
    }

    /// <summary>
    /// Registers a drawing strategy in the toolbar.
    /// </summary>
    /// <param name="button">The button associated with the strategy.</param>
    /// <param name="tool">The drawing tool implementation.</param>
    private void RegisterTool(ToolStripButton button, IDrawStrategy tool)
    {
        ArgumentNullException.ThrowIfNull(button);
        ArgumentNullException.ThrowIfNull(tool);

        _toolBindings[button] = tool;
        button.Click += ToolButton_Click;
    }

    private void ToolButton_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripButton button)
        {
            ExecuteSafely(() => SelectTool(button), "selectarea instrumentului");
        }
    }

    /// <summary>
    /// Marks a tool as active in the toolbar and stores the selected strategy.
    /// </summary>
    /// <param name="selectedButton">The toolbar button selected by the user.</param>
    private void SelectTool(ToolStripButton selectedButton)
    {
        if (!_toolBindings.ContainsKey(selectedButton))
        {
            throw new InvalidOperationException("Instrumentul selectat nu este inregistrat.");
        }

        foreach (var button in _toolBindings.Keys)
        {
            button.Checked = button == selectedButton;
        }

        _currentTool = _toolBindings[selectedButton];
    }

    private void ThicknessToolStripComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (float.TryParse(thicknessToolStripComboBox.Text, out var thickness))
        {
            _selectedThickness = thickness;
        }
    }

    private void ColorToolStripButton_Click(object? sender, EventArgs e)
    {
        ExecuteSafely(ChooseColor, "alegerea culorii");
    }

    private void SaveToolStripButton_Click(object? sender, EventArgs e)
    {
        ExecuteSafely(SaveCanvasToDisk, "salvarea imaginii");
    }

    private void UndoToolStripButton_Click(object? sender, EventArgs e)
    {
        ExecuteSafely(_historyManager.Undo, "operatia Undo");
    }

    private void RedoToolStripButton_Click(object? sender, EventArgs e)
    {
        ExecuteSafely(_historyManager.Redo, "operatia Redo");
    }

    private void CanvasPanel_Resize(object? sender, EventArgs e)
    {
        ExecuteSafely(() => EnsureCanvasSize(canvasPanel.ClientSize), "redimensionarea canvas-ului");
    }

    private void CanvasPanel_Paint(object? sender, PaintEventArgs e)
    {
        e.Graphics.Clear(Color.White);

        var imageToDraw = _previewImage ?? _currentImage;
        if (imageToDraw is not null)
        {
            e.Graphics.DrawImageUnscaled(imageToDraw, Point.Empty);
        }
    }

    private void CanvasPanel_MouseDown(object? sender, MouseEventArgs e)
    {
        ExecuteSafely(() => BeginDrawing(e), "inceperea desenarii");
    }

    private void CanvasPanel_MouseMove(object? sender, MouseEventArgs e)
    {
        ExecuteSafely(() => ContinueDrawing(e), "actualizarea desenului");
    }

    private void CanvasPanel_MouseUp(object? sender, MouseEventArgs e)
    {
        ExecuteSafely(() => EndDrawing(e), "finalizarea desenului");
    }

    /// <summary>
    /// Opens the color dialog and stores the chosen color.
    /// </summary>
    private void ChooseColor()
    {
        colorDialog.Color = _selectedColor;

        if (colorDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _selectedColor = colorDialog.Color;
        UpdateColorButton();
    }

    /// <summary>
    /// Handles the first mouse click that starts a new drawing operation.
    /// </summary>
    /// <param name="e">Mouse data captured by the canvas event.</param>
    private void BeginDrawing(MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || _currentTool is null)
        {
            return;
        }

        EnsureCanvasSize(canvasPanel.ClientSize);
        if (_currentImage is null)
        {
            return;
        }

        _previewImage?.Dispose();
        _previewImage = null;

        _drawingStartImage?.Dispose();
        _drawingStartImage = (Bitmap)_currentImage.Clone();

        _startPoint = ClampPointToCanvas(e.Location);
        _lastPoint = _startPoint;
        _isDrawing = true;
        canvasPanel.Capture = true;

        // Freehand drawing starts immediately so a single click can still leave a visible dot.
        if (_currentTool.DrawsContinuously)
        {
            DrawOnBitmap(_currentImage, _startPoint, _startPoint);
            canvasPanel.Invalidate();
        }
    }

    /// <summary>
    /// Updates the preview or the freehand stroke while the mouse moves.
    /// </summary>
    /// <param name="e">Mouse data captured by the canvas event.</param>
    private void ContinueDrawing(MouseEventArgs e)
    {
        if (!_isDrawing || _currentTool is null || _currentImage is null || _drawingStartImage is null)
        {
            return;
        }

        var currentPoint = ClampPointToCanvas(e.Location);

        if (_currentTool.DrawsContinuously)
        {
            DrawOnBitmap(_currentImage, _lastPoint, currentPoint);
            _lastPoint = currentPoint;
        }
        else
        {
            _previewImage?.Dispose();
            _previewImage = (Bitmap)_drawingStartImage.Clone();
            DrawOnBitmap(_previewImage, _startPoint, currentPoint);
        }

        canvasPanel.Invalidate();
    }

    /// <summary>
    /// Finalizes the drawing operation and stores it in the history stack.
    /// </summary>
    /// <param name="e">Mouse data captured by the canvas event.</param>
    private void EndDrawing(MouseEventArgs e)
    {
        if (!_isDrawing || _currentTool is null || _currentImage is null || _drawingStartImage is null)
        {
            return;
        }

        var endPoint = ClampPointToCanvas(e.Location);

        if (!_currentTool.DrawsContinuously)
        {
            _previewImage?.Dispose();
            _previewImage = null;
            DrawOnBitmap(_currentImage, _startPoint, endPoint);
        }

        CommitCurrentDrawing();
        ResetTransientDrawingState();
        canvasPanel.Invalidate();
    }

    /// <summary>
    /// Draws the current tool on the specified bitmap.
    /// </summary>
    /// <param name="targetBitmap">The bitmap that receives the drawing.</param>
    /// <param name="start">The start point of the drawing gesture.</param>
    /// <param name="end">The end point of the drawing gesture.</param>
    /// <exception cref="ArgumentNullException">Thrown when the bitmap is null.</exception>
    private void DrawOnBitmap(Bitmap targetBitmap, Point start, Point end)
    {
        ArgumentNullException.ThrowIfNull(targetBitmap);

        if (_currentTool is null)
        {
            return;
        }

        using var graphics = Graphics.FromImage(targetBitmap);
        using var pen = CreateActivePen();

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        _currentTool.Draw(graphics, start, end, pen);
    }

    /// <summary>
    /// Creates the pen used by the active drawing tool.
    /// </summary>
    /// <returns>A pen configured with the currently selected color and thickness.</returns>
    private Pen CreateActivePen()
    {
        return new Pen(_selectedColor, _selectedThickness)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
    }

    /// <summary>
    /// Commits the current drawing gesture to the Undo/Redo history.
    /// </summary>
    private void CommitCurrentDrawing()
    {
        if (_currentImage is null || _drawingStartImage is null)
        {
            return;
        }

        var previousState = _drawingStartImage;
        _drawingStartImage = null;

        try
        {
            _historyManager.ExecuteCommand(new DrawCommand(previousState, _currentImage, this));
        }
        finally
        {
            previousState.Dispose();
        }
    }

    /// <summary>
    /// Ensures the internal canvas bitmap is large enough for the visible panel.
    /// </summary>
    /// <param name="requestedSize">The space required by the canvas control.</param>
    private void EnsureCanvasSize(Size requestedSize)
    {
        var width = Math.Max(requestedSize.Width, 1);
        var height = Math.Max(requestedSize.Height, 1);

        if (_currentImage is null)
        {
            _currentImage = CreateBlankCanvas(width, height);
            canvasPanel.Invalidate();
            return;
        }

        if (_currentImage.Width >= width && _currentImage.Height >= height)
        {
            return;
        }

        var expandedCanvas = CreateBlankCanvas(Math.Max(_currentImage.Width, width), Math.Max(_currentImage.Height, height));
        using (var graphics = Graphics.FromImage(expandedCanvas))
        {
            graphics.DrawImageUnscaled(_currentImage, Point.Empty);
        }

        _currentImage.Dispose();
        _currentImage = expandedCanvas;
        canvasPanel.Invalidate();
    }

    /// <summary>
    /// Saves the current canvas image to disk.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">Thrown when the destination is not writable.</exception>
    /// <exception cref="IOException">Thrown when the file operation fails.</exception>
    /// <exception cref="ExternalException">Thrown when GDI+ cannot encode the target image.</exception>
    private void SaveCanvasToDisk()
    {
        if (_currentImage is null)
        {
            MessageBox.Show(this, "Nu exista inca nimic de salvat.", "Informatie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        saveFileDialog.FileName = $"desen_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        using var imageToSave = (Bitmap)_currentImage.Clone();
        try
        {
            imageToSave.Save(saveFileDialog.FileName, GetImageFormat(saveFileDialog.FileName));
            MessageBox.Show(this, "Imaginea a fost salvata cu succes.", "Salvare reusita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (UnauthorizedAccessException exception)
        {
            ShowOperationError("accesarea locatiei de salvare", exception);
        }
        catch (IOException exception)
        {
            ShowOperationError("scrierea fisierului pe disc", exception);
        }
        catch (ExternalException exception)
        {
            ShowOperationError("codificarea imaginii", exception);
        }
    }

    /// <summary>
    /// Resolves the image format based on the output file extension.
    /// </summary>
    /// <param name="filePath">The path selected by the user.</param>
    /// <returns>The matching image format.</returns>
    /// <exception cref="ArgumentException">Thrown when the file path is missing.</exception>
    private static ImageFormat GetImageFormat(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Calea fisierului nu poate fi vida.", nameof(filePath));
        }

        return Path.GetExtension(filePath).ToLowerInvariant() switch
        {
            ".bmp" => ImageFormat.Bmp,
            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
            _ => ImageFormat.Png
        };
    }

    /// <summary>
    /// Updates the color preview shown inside the toolbar.
    /// </summary>
    private void UpdateColorButton()
    {
        colorToolStripButton.BackColor = _selectedColor;
        colorToolStripButton.ForeColor = _selectedColor.GetBrightness() < 0.45f ? Color.White : Color.Black;
    }

    private void HistoryManager_HistoryChanged(object? sender, EventArgs e)
    {
        UpdateHistoryButtons();
    }

    /// <summary>
    /// Enables or disables Undo/Redo buttons according to the current history state.
    /// </summary>
    private void UpdateHistoryButtons()
    {
        undoToolStripButton.Enabled = _historyManager.CanUndo;
        redoToolStripButton.Enabled = _historyManager.CanRedo;
    }

    /// <summary>
    /// Keeps the mouse position inside the bitmap boundaries.
    /// </summary>
    /// <param name="point">The raw mouse position.</param>
    /// <returns>A point clamped to the valid bitmap area.</returns>
    private Point ClampPointToCanvas(Point point)
    {
        if (_currentImage is null)
        {
            return point;
        }

        var clampedX = Math.Clamp(point.X, 0, _currentImage.Width - 1);
        var clampedY = Math.Clamp(point.Y, 0, _currentImage.Height - 1);
        return new Point(clampedX, clampedY);
    }

    /// <summary>
    /// Creates a new blank canvas with a white background.
    /// </summary>
    /// <param name="width">Canvas width in pixels.</param>
    /// <param name="height">Canvas height in pixels.</param>
    /// <returns>A white bitmap ready for drawing.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when width or height is less than one.</exception>
    private static Bitmap CreateBlankCanvas(int width, int height)
    {
        if (width < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Latimea canvas-ului trebuie sa fie cel putin 1.");
        }

        if (height < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(height), "Inaltimea canvas-ului trebuie sa fie cel putin 1.");
        }

        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        return bitmap;
    }

    /// <summary>
    /// Executes a UI operation under a common exception handling policy.
    /// </summary>
    /// <param name="action">The operation to execute.</param>
    /// <param name="operationName">A user-friendly name of the operation.</param>
    private void ExecuteSafely(Action action, string operationName)
    {
        try
        {
            action();
        }
        catch (Exception exception)
        {
            ResetTransientDrawingState();
            ShowOperationError(operationName, exception);
        }
    }

    /// <summary>
    /// Restores a neutral canvas interaction state after an error or a completed gesture.
    /// </summary>
    private void ResetTransientDrawingState()
    {
        _isDrawing = false;
        canvasPanel.Capture = false;

        _previewImage?.Dispose();
        _previewImage = null;

        _drawingStartImage?.Dispose();
        _drawingStartImage = null;
    }

    /// <summary>
    /// Displays a consistent error message for end-user operations.
    /// </summary>
    /// <param name="operationName">The failed operation.</param>
    /// <param name="exception">The captured exception.</param>
    private void ShowOperationError(string operationName, Exception exception)
    {
        MessageBox.Show(
            this,
            $"A aparut o eroare la {operationName}:\n{exception.Message}",
            "Eroare",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    /// <summary>
    /// Releases disposable resources owned by the form.
    /// </summary>
    private void DisposeCanvasResources()
    {
        _historyManager.HistoryChanged -= HistoryManager_HistoryChanged;
        _historyManager.Dispose();
        _drawingStartImage?.Dispose();
        _previewImage?.Dispose();
        _currentImage?.Dispose();
    }

    private void HelpToolStripButton_Click(object sender, EventArgs e)
    {
        Help.ShowHelp(this, "SharpPaintHelp.chm");
    }
}
