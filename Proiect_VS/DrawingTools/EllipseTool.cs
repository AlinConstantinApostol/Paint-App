using System.Drawing;

namespace DrawingTools;

public sealed class EllipseTool : ShapeToolBase
{
    public override string DisplayName => "Cerc";

    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawEllipse(currentPen, NormalizeRectangle(start, end));
    }
}
