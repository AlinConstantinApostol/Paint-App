// PencilTool.cs - freehand drawing strategy.

using System.Drawing;

namespace DrawingTools;

/// <summary>
/// Draws continuous freehand strokes between successive mouse points.
/// </summary>
public sealed class PencilTool : IDrawStrategy
{
    /// <inheritdoc />
    public string DisplayName => "Creion";

    /// <inheritdoc />
    public bool DrawsContinuously => true;

    /// <inheritdoc />
    public void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
