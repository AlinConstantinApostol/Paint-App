/*************************************************************************
 *                                                                       *
 * File:          TestCanvasHost.cs                                      *
 * Copyright:     (c) 2026, Buzatu Stefan                                *
 * E-mail:        stefan.buzatu2@student.tuiasi.ro                       *
 * Description:   Implements a simple fake canvas host for testing the   *
 *                history manager.                                       *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/
using System.Drawing;
using StateManager;

namespace PaintApp.Tests;

/// <summary>
/// Simple fake canvas host used by the history tests.
/// </summary>
internal sealed class TestCanvasHost : ICanvasHost, IDisposable
{
    public Bitmap? CurrentImage { get; private set; }

    public int SetCalls { get; private set; }

    public void SetCanvasImage(Bitmap image)
    {
        CurrentImage?.Dispose();
        CurrentImage = image;
        SetCalls++;
    }

    public void Dispose()
    {
        CurrentImage?.Dispose();
    }
}
