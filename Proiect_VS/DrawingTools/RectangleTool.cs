// RectangleTool.cs - rectangle drawing strategy.

using System.Drawing;

namespace DrawingTools;

/// <summary>
/// Draws a rectangle defined by the normalized bounds of two user-selected points.
/// </summary>
public sealed class RectangleTool : ShapeToolBase
{
    /// <inheritdoc />
    public override string DisplayName => "Dreptunghi";

    /// <inheritdoc />
    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawRectangle(currentPen, NormalizeRectangle(start, end));
    }
}
