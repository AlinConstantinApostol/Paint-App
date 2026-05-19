/*************************************************************************
 *                                                                       *
 * File:          LineTool.cs                                            *
 * Copyright:     (c) 2026, Basu Stefan                                  *
 * E-mail:        stefan.basu@student.tuiasi.ro                          *
 * Description:   Implements the LineTool class straight lines.          *
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
/// Draws a straight line between the start and end points.
/// </summary>
public sealed class LineTool : ShapeToolBase
{
    /// <inheritdoc />
    public override string DisplayName => "Linie";

    /// <inheritdoc />
    public override void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
