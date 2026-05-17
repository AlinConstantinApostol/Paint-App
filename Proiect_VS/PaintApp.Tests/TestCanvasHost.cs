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
