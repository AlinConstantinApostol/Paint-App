// EllipseTool.cs - ellipse/circle drawing strategy.

using System.Drawing;

namespace DrawingTools;

/// <summary>
/// Draws an ellipse inside the normalized rectangle generated from two points.
/// </summary>
public sealed class EllipseTool : ShapeToolBase
{
    /// <inheritdoc />
    public override string DisplayName => "Cerc";

    /// <inheritdoc />
    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawEllipse(currentPen, NormalizeRectangle(start, end));
    }
}
