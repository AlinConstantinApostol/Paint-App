namespace PaintApp;

partial class MainCanvasForm
{
    private System.ComponentModel.IContainer components = null;
    private ToolStrip mainToolStrip;
    private ToolStripButton saveToolStripButton;
    private ToolStripButton undoToolStripButton;
    private ToolStripButton redoToolStripButton;
    private ToolStripSeparator saveToolStripSeparator;
    private ToolStripButton pencilToolStripButton;
    private ToolStripButton lineToolStripButton;
    private ToolStripButton rectangleToolStripButton;
    private ToolStripButton ellipseToolStripButton;
    private ToolStripSeparator toolsToolStripSeparator;
    private ToolStripButton colorToolStripButton;
    private ToolStripLabel thicknessToolStripLabel;
    private ToolStripComboBox thicknessToolStripComboBox;
    private DoubleBufferedPanel canvasPanel;
    private SaveFileDialog saveFileDialog;
    private ColorDialog colorDialog;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DisposeCanvasResources();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        mainToolStrip = new ToolStrip();
        saveToolStripButton = new ToolStripButton();
        undoToolStripButton = new ToolStripButton();
        redoToolStripButton = new ToolStripButton();
        saveToolStripSeparator = new ToolStripSeparator();
        pencilToolStripButton = new ToolStripButton();
        lineToolStripButton = new ToolStripButton();
        rectangleToolStripButton = new ToolStripButton();
        ellipseToolStripButton = new ToolStripButton();
        toolsToolStripSeparator = new ToolStripSeparator();
        colorToolStripButton = new ToolStripButton();
        thicknessToolStripLabel = new ToolStripLabel();
        thicknessToolStripComboBox = new ToolStripComboBox();
        canvasPanel = new DoubleBufferedPanel();
        saveFileDialog = new SaveFileDialog();
        colorDialog = new ColorDialog();
        mainToolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // mainToolStrip
        // 
        mainToolStrip.GripStyle = ToolStripGripStyle.Hidden;
        mainToolStrip.ImageScalingSize = new Size(20, 20);
        mainToolStrip.Items.AddRange(new ToolStripItem[] { saveToolStripButton, undoToolStripButton, redoToolStripButton, saveToolStripSeparator, pencilToolStripButton, lineToolStripButton, rectangleToolStripButton, ellipseToolStripButton, toolsToolStripSeparator, colorToolStripButton, thicknessToolStripLabel, thicknessToolStripComboBox });
        mainToolStrip.Location = new Point(0, 0);
        mainToolStrip.Name = "mainToolStrip";
        mainToolStrip.Padding = new Padding(8, 4, 8, 4);
        mainToolStrip.Size = new Size(1184, 38);
        mainToolStrip.TabIndex = 0;
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Size = new Size(63, 27);
        saveToolStripButton.Text = "Salveaza";
        // 
        // undoToolStripButton
        // 
        undoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        undoToolStripButton.Name = "undoToolStripButton";
        undoToolStripButton.Size = new Size(45, 27);
        undoToolStripButton.Text = "Undo";
        // 
        // redoToolStripButton
        // 
        redoToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        redoToolStripButton.Name = "redoToolStripButton";
        redoToolStripButton.Size = new Size(44, 27);
        redoToolStripButton.Text = "Redo";
        // 
        // pencilToolStripButton
        // 
        pencilToolStripButton.CheckOnClick = true;
        pencilToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        pencilToolStripButton.Name = "pencilToolStripButton";
        pencilToolStripButton.Size = new Size(47, 27);
        pencilToolStripButton.Text = "Creion";
        // 
        // lineToolStripButton
        // 
        lineToolStripButton.CheckOnClick = true;
        lineToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        lineToolStripButton.Name = "lineToolStripButton";
        lineToolStripButton.Size = new Size(38, 27);
        lineToolStripButton.Text = "Linie";
        // 
        // rectangleToolStripButton
        // 
        rectangleToolStripButton.CheckOnClick = true;
        rectangleToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        rectangleToolStripButton.Name = "rectangleToolStripButton";
        rectangleToolStripButton.Size = new Size(79, 27);
        rectangleToolStripButton.Text = "Dreptunghi";
        // 
        // ellipseToolStripButton
        // 
        ellipseToolStripButton.CheckOnClick = true;
        ellipseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        ellipseToolStripButton.Name = "ellipseToolStripButton";
        ellipseToolStripButton.Size = new Size(39, 27);
        ellipseToolStripButton.Text = "Cerc";
        // 
        // colorToolStripButton
        // 
        colorToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
        colorToolStripButton.Name = "colorToolStripButton";
        colorToolStripButton.Size = new Size(54, 27);
        colorToolStripButton.Text = "Culoare";
        // 
        // thicknessToolStripLabel
        // 
        thicknessToolStripLabel.Name = "thicknessToolStripLabel";
        thicknessToolStripLabel.Size = new Size(56, 27);
        thicknessToolStripLabel.Text = "Grosime";
        // 
        // thicknessToolStripComboBox
        // 
        thicknessToolStripComboBox.AutoSize = false;
        thicknessToolStripComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        thicknessToolStripComboBox.Items.AddRange(new object[] { "2", "4", "6", "8" });
        thicknessToolStripComboBox.Name = "thicknessToolStripComboBox";
        thicknessToolStripComboBox.Size = new Size(75, 28);
        // 
        // canvasPanel
        // 
        canvasPanel.BackColor = Color.White;
        canvasPanel.BorderStyle = BorderStyle.FixedSingle;
        canvasPanel.Dock = DockStyle.Fill;
        canvasPanel.Location = new Point(0, 38);
        canvasPanel.Name = "canvasPanel";
        canvasPanel.Size = new Size(1184, 623);
        canvasPanel.TabIndex = 1;
        canvasPanel.Paint += CanvasPanel_Paint;
        canvasPanel.MouseDown += CanvasPanel_MouseDown;
        canvasPanel.MouseMove += CanvasPanel_MouseMove;
        canvasPanel.MouseUp += CanvasPanel_MouseUp;
        canvasPanel.Resize += CanvasPanel_Resize;
        // 
        // saveFileDialog
        // 
        saveFileDialog.DefaultExt = "png";
        saveFileDialog.Filter = "PNG Image|*.png|Bitmap Image|*.bmp|JPEG Image|*.jpg";
        saveFileDialog.Title = "Salveaza desenul";
        // 
        // MainCanvasForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(236, 236, 236);
        ClientSize = new Size(1184, 661);
        Controls.Add(canvasPanel);
        Controls.Add(mainToolStrip);
        MinimumSize = new Size(920, 600);
        Name = "MainCanvasForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Paint App - IP";
        mainToolStrip.ResumeLayout(false);
        mainToolStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
