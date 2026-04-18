using System.Drawing;

namespace DrawingTools;

public sealed class PencilTool : IDrawStrategy
{
    public string DisplayName => "Creion";

    public bool DrawsContinuously => true;

    public void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
