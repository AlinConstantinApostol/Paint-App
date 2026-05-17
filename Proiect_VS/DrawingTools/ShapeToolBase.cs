// ShapeToolBase.cs - reusable helpers for tools based on normalized bounding rectangles.

using System.Drawing;

namespace DrawingTools;

/// <summary>
/// Provides shared behavior for drawing tools that operate on a bounding rectangle.
/// </summary>
public abstract class ShapeToolBase : IDrawStrategy
{
    /// <inheritdoc />
    public abstract string DisplayName { get; }

    /// <inheritdoc />
    public virtual bool DrawsContinuously => false;

    /// <inheritdoc />
    public abstract void Draw(Graphics graphics, Point start, Point end, Pen currentPen);

    /// <summary>
    /// Converts two arbitrary points into a rectangle with positive width and height.
    /// </summary>
    /// <param name="start">The first corner selected by the user.</param>
    /// <param name="end">The opposite corner selected by the user.</param>
    /// <returns>A normalized rectangle.</returns>
    protected static Rectangle NormalizeRectangle(Point start, Point end)
    {
        var left = Math.Min(start.X, end.X);
        var top = Math.Min(start.Y, end.Y);
        var width = Math.Abs(end.X - start.X);
        var height = Math.Abs(end.Y - start.Y);

        return new Rectangle(left, top, width, height);
    }
}
