using System.Drawing;

namespace StateManager;

public sealed class DrawCommand : ICommand, IDisposable
{
    private readonly Bitmap _previousState;
    private readonly Bitmap _newState;
    private readonly ICanvasHost _targetCanvas;

    public DrawCommand(Bitmap previousState, Bitmap newState, ICanvasHost targetCanvas)
    {
        _previousState = (Bitmap)previousState.Clone();
        _newState = (Bitmap)newState.Clone();
        _targetCanvas = targetCanvas;
    }

    public void Execute()
    {
        _targetCanvas.SetCanvasImage((Bitmap)_newState.Clone());
    }

    public void Undo()
    {
        _targetCanvas.SetCanvasImage((Bitmap)_previousState.Clone());
    }

    public void Dispose()
    {
        _previousState.Dispose();
        _newState.Dispose();
    }
}
