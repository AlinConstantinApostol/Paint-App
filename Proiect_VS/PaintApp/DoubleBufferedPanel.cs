/*
 * DoubleBufferedPanel.cs
 * Lightweight helper control used to reduce flicker while painting.
 */

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
