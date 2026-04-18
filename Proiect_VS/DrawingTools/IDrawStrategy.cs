using System.Drawing;

namespace DrawingTools;

public interface IDrawStrategy
{
    string DisplayName { get; }

    bool DrawsContinuously { get; }

    void Draw(Graphics graphics, Point start, Point end, Pen currentPen);
}
