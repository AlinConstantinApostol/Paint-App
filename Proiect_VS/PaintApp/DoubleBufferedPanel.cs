/*************************************************************************
 *                                                                       *
 * File:          DoubleBufferedPanel.cs                                 *
 * Copyright:     (c) 2026, Vrinceanu Sterica                            *
 * E-mail:        sterica.vrinceanu@student.tuiasi.ro                    *
 * Description:   Implements a custom panel with double buffering        *
 *                enabled to reduce flicker during drawing operations.   *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/
namespace PaintApp;

/// <summary>
/// Provides a panel with double buffering enabled for smoother canvas rendering.
/// </summary>
internal sealed class DoubleBufferedPanel : Panel
{
    /// <summary>
    /// Initializes the panel with double buffering and automatic redraw on resize.
    /// </summary>
    public DoubleBufferedPanel()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
    }
}
