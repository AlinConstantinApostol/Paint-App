using System.Drawing;

namespace DrawingTools;

public abstract class ShapeToolBase : IDrawStrategy
{
    public abstract string DisplayName { get; }

    public virtual bool DrawsContinuously => false;

    public abstract void Draw(Graphics graphics, Point start, Point end, Pen currentPen);

    protected static Rectangle NormalizeRectangle(Point start, Point end)
    {
        var left = Math.Min(start.X, end.X);
        var top = Math.Min(start.Y, end.Y);
        var width = Math.Abs(end.X - start.X);
        var height = Math.Abs(end.Y - start.Y);

        return new Rectangle(left, top, width, height);
    }
}
