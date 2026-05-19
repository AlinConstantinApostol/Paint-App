/*************************************************************************
 *                                                                       *
 * File:          PencilTool.cs                                          *
 * Copyright:     (c) 2026, Basu Stefan                                  *
 * E-mail:        stefan.basu@student.tuiasi.ro                          *
 * Description:   Implements the PencilTool class for freehand drawing.  *
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
/// Draws continuous freehand strokes between successive mouse points.
/// </summary>
public sealed class PencilTool : IDrawStrategy
{
    /// <inheritdoc />
    public string DisplayName => "Creion";

    /// <inheritdoc />
    public bool DrawsContinuously => true;

    /// <inheritdoc />
    public void Draw(Graphics graphics, Point start, Point end, Pen currentPen)
    {
        graphics.DrawLine(currentPen, start, end);
    }
}
