/*************************************************************************
 *                                                                       *
 * File:          ShapeToolBase.cs                                       *
 * Copyright:     (c) 2026, Basu Stefan                                  *
 * E-mail:        stefan.basu@student.tuiasi.ro                          *
 * Description:   Implements the ShapeToolBase class for common shapes.  *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/

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
