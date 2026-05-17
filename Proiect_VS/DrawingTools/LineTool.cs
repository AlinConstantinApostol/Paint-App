// LineTool.cs - straight line drawing strategy.

using System.Drawing;

namespace DrawingTools;

/// <summary>
/// Draws a straight line between the start and end points.
/// </summary>
public sealed class LineTool : ShapeToolBase
{
    /// <inheritdoc />
    public override string DisplayName => "Linie";

    /// <inheritdoc />
    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
