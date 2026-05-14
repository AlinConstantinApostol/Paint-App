// ICanvasHost.cs - abstraction used by history commands to replace the visible image.

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
