using System.Drawing;

namespace DrawingTools;

public sealed class LineTool : ShapeToolBase
{
    public override string DisplayName => "Linie";

    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
