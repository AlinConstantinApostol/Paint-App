/*************************************************************************
 *                                                                       *
 * File:          ICanvasHost.cs                                         *
 * Copyright:     (c) 2026, Apostol Alin-Constantin                      *
 * E-mail:        alin-constantin.apostol@student.tuiasi.ro              *
 * Description:   Defines the ICanvasHost interface for accepting        *
 *                canvas images.                                         *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/

using System.Drawing;

namespace StateManager;

/// <summary>
/// Represents an object capable of accepting a bitmap as the current canvas image.
/// </summary>
public interface ICanvasHost
{
    /// <summary>
    /// Applies a bitmap as the active image shown by the canvas owner.
    /// </summary>
    /// <param name="image">The bitmap to display.</param>
    void SetCanvasImage(Bitmap image);
}
