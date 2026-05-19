/*************************************************************************
 *                                                                       *
 * File:          IDrawStrategy.cs                                       *
 * Copyright:     (c) 2026, Basu Stefan                                  *
 * E-mail:        stefan.basu@student.tuiasi.ro                          *
 * Description:   Defines the IDrawStrategy interface for drawing shapes.*
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
/// Defines the common behavior required by any drawing tool strategy.
/// </summary>
public interface IDrawStrategy
{
    /// <summary>
    /// Gets the user-friendly tool name shown in the GUI.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Gets a value indicating whether the tool draws continuously while the mouse moves.
    /// </summary>
    bool DrawsContinuously { get; }

    /// <summary>
    /// Draws the tool output on the supplied graphics surface.
    /// </summary>
    /// <param name="graphics">The target graphics surface.</param>
    /// <param name="start">The gesture start point.</param>
    /// <param name="end">The gesture end point.</param>
    /// <param name="currentPen">The pen used for rendering.</param>
    void Draw(Graphics graphics, Point start, Point end, Pen currentPen);
}
