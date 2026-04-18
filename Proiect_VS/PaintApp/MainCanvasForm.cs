using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DrawingTools;
using StateManager;

namespace PaintApp;

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

    public MainCanvasForm()
    {
        InitializeComponent();
        ConfigureApplication();
    }

    public void SetCanvasImage(Bitmap image)
    {
        _previewImage?.Dispose();
        _previewImage = null;

        _currentImage?.Dispose();
        _currentImage = image;

        canvasPanel.Invalidate();
    }

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

    private void RegisterTool(ToolStripButton button, IDrawStrategy tool)
    {
        _toolBindings[button] = tool;
        button.Click += ToolButton_Click;
    }

    private void ToolButton_Click(object? sender, EventArgs e)
    {
        if (sender is ToolStripButton button)
        {
            SelectTool(button);
        }
    }

    private void SelectTool(ToolStripButton selectedButton)
    {
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
        colorDialog.Color = _selectedColor;

        if (colorDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        _selectedColor = colorDialog.Color;
        UpdateColorButton();
    }

    private void SaveToolStripButton_Click(object? sender, EventArgs e)
    {
        SaveCanvasToDisk();
    }

    private void UndoToolStripButton_Click(object? sender, EventArgs e)
    {
        _historyManager.Undo();
    }

    private void RedoToolStripButton_Click(object? sender, EventArgs e)
    {
        _historyManager.Redo();
    }

    private void CanvasPanel_Resize(object? sender, EventArgs e)
    {
        EnsureCanvasSize(canvasPanel.ClientSize);
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

        if (_currentTool.DrawsContinuously)
        {
            DrawOnBitmap(_currentImage, _startPoint, _startPoint);
            canvasPanel.Invalidate();
        }
    }

    private void CanvasPanel_MouseMove(object? sender, MouseEventArgs e)
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

    private void CanvasPanel_MouseUp(object? sender, MouseEventArgs e)
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
        _isDrawing = false;
        canvasPanel.Capture = false;
        canvasPanel.Invalidate();
    }

    private void DrawOnBitmap(Bitmap targetBitmap, Point start, Point end)
    {
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

    private Pen CreateActivePen()
    {
        return new Pen(_selectedColor, _selectedThickness)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
    }

    private void CommitCurrentDrawing()
    {
        if (_currentImage is null || _drawingStartImage is null)
        {
            return;
        }

        var previousState = _drawingStartImage;
        _drawingStartImage = null;

        _historyManager.ExecuteCommand(new DrawCommand(previousState, _currentImage, this));
        previousState.Dispose();
    }

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

    private void SaveCanvasToDisk()
    {
        if (_currentImage is null)
        {
            return;
        }

        saveFileDialog.FileName = $"desen_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            _currentImage.Save(saveFileDialog.FileName, GetImageFormat(saveFileDialog.FileName));
            MessageBox.Show(this, "Imaginea a fost salvata cu succes.", "Salvare reusita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(this, $"Salvarea imaginii a esuat:\n{exception.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static ImageFormat GetImageFormat(string filePath)
    {
        return Path.GetExtension(filePath).ToLowerInvariant() switch
        {
            ".bmp" => ImageFormat.Bmp,
            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
            _ => ImageFormat.Png
        };
    }

    private void UpdateColorButton()
    {
        colorToolStripButton.BackColor = _selectedColor;
        colorToolStripButton.ForeColor = _selectedColor.GetBrightness() < 0.45f ? Color.White : Color.Black;
    }

    private void HistoryManager_HistoryChanged(object? sender, EventArgs e)
    {
        UpdateHistoryButtons();
    }

    private void UpdateHistoryButtons()
    {
        undoToolStripButton.Enabled = _historyManager.CanUndo;
        redoToolStripButton.Enabled = _historyManager.CanRedo;
    }

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

    private static Bitmap CreateBlankCanvas(int width, int height)
    {
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        return bitmap;
    }

    private void DisposeCanvasResources()
    {
        _historyManager.HistoryChanged -= HistoryManager_HistoryChanged;
        _historyManager.Dispose();
        _drawingStartImage?.Dispose();
        _previewImage?.Dispose();
        _currentImage?.Dispose();
    }
}
