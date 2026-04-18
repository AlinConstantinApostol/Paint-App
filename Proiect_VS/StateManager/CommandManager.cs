namespace StateManager;

public sealed class CommandManager : IDisposable
{
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    public event EventHandler? HistoryChanged;

    public bool CanUndo => _undoStack.Count > 0;

    public bool CanRedo => _redoStack.Count > 0;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
        ClearStack(_redoStack);
        OnHistoryChanged();
    }

    public void Undo()
    {
        if (!CanUndo)
        {
            return;
        }

        var command = _undoStack.Pop();
        command.Undo();
        _redoStack.Push(command);
        OnHistoryChanged();
    }

    public void Redo()
    {
        if (!CanRedo)
        {
            return;
        }

        var command = _redoStack.Pop();
        command.Execute();
        _undoStack.Push(command);
        OnHistoryChanged();
    }

    public void ClearHistory()
    {
        ClearStack(_undoStack);
        ClearStack(_redoStack);
        OnHistoryChanged();
    }

    public void Dispose()
    {
        ClearHistory();
    }

    private static void ClearStack(Stack<ICommand> stack)
    {
        while (stack.Count > 0)
        {
            if (stack.Pop() is IDisposable disposableCommand)
            {
                disposableCommand.Dispose();
            }
        }
    }

    private void OnHistoryChanged()
    {
        HistoryChanged?.Invoke(this, EventArgs.Empty);
    }
}
