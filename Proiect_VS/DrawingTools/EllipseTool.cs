/*************************************************************************
 *                                                                       *
 * File:          ElilpseTool.cs                                         *
 * Copyright:     (c) 2026, Basu Stefan                                  *
 * E-mail:        stefan.basu@student.tuiasi.ro                          *
 * Description:   Implements the EllipseTool class for drawing ellipses. *
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
/// Draws an ellipse inside the normalized rectangle generated from two points.
/// </summary>
public sealed class EllipseTool : ShapeToolBase
{
    /// <inheritdoc />
    public override string DisplayName => "Cerc";

    /// <inheritdoc />
    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawEllipse(currentPen, NormalizeRectangle(start, end));
    }
}
