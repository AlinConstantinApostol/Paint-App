using System.Drawing;

namespace DrawingTools;

public sealed class RectangleTool : ShapeToolBase
{
    public override string DisplayName => "Dreptunghi";

    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawRectangle(currentPen, NormalizeRectangle(start, end));
    }
}
