/*
 * DrawCommand.cs
 * Command implementation that stores the previous and next canvas state.
 */

using System.Drawing;

namespace StateManager;

/// <summary>
/// Encapsulates a drawing transition from one bitmap state to another.
/// </summary>
public sealed class DrawCommand : ICommand, IDisposable
{
    private readonly Bitmap _previousState;
    private readonly Bitmap _newState;
    private readonly ICanvasHost _targetCanvas;

    /// <summary>
    /// Initializes a new drawing command from a previous and a new bitmap state.
    /// </summary>
    /// <param name="previousState">The image shown before the drawing action.</param>
    /// <param name="newState">The image shown after the drawing action.</param>
    /// <param name="targetCanvas">The canvas that receives the bitmap state.</param>
    /// <exception cref="ArgumentNullException">Thrown when a dependency is null.</exception>
    public DrawCommand(Bitmap previousState, Bitmap newState, ICanvasHost targetCanvas)
    {
        ArgumentNullException.ThrowIfNull(previousState);
        ArgumentNullException.ThrowIfNull(newState);
        ArgumentNullException.ThrowIfNull(targetCanvas);

        _previousState = (Bitmap)previousState.Clone();
        _newState = (Bitmap)newState.Clone();
        _targetCanvas = targetCanvas;
    }

    /// <inheritdoc />
    public void Execute()
    {
        _targetCanvas.SetCanvasImage((Bitmap)_newState.Clone());
    }

    /// <inheritdoc />
    public void Undo()
    {
        _targetCanvas.SetCanvasImage((Bitmap)_previousState.Clone());
    }

    /// <summary>
    /// Releases internal bitmap snapshots kept by the command.
    /// </summary>
    public void Dispose()
    {
        _previousState.Dispose();
        _newState.Dispose();
    }
}
