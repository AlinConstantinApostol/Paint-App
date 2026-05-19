/*************************************************************************
 *                                                                       *
 * File:          CommandManager.cs                                      *
 * Copyright:     (c) 2026, Apostol Alin-Constantin                      *
 * E-mail:        alin-constantin.apostol@student.tuiasi.ro              *
 * Description:   Defines the CommandManager class for managing command  * 
 *                history and Undo/Redo behavior.                        *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/

namespace StateManager;

/// <summary>
/// Stores executed commands and exposes Undo/Redo behavior.
/// </summary>
public sealed class CommandManager : IDisposable
{
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    /// <summary>
    /// Raised whenever the Undo/Redo state changes.
    /// </summary>
    public event EventHandler? HistoryChanged;

    /// <summary>
    /// Gets a value indicating whether there is at least one command to undo.
    /// </summary>
    public bool CanUndo => _undoStack.Count > 0;

    /// <summary>
    /// Gets a value indicating whether there is at least one command to redo.
    /// </summary>
    public bool CanRedo => _redoStack.Count > 0;

    /// <summary>
    /// Executes a command and pushes it to the undo stack.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <exception cref="ArgumentNullException">Thrown when the command is null.</exception>
    public void ExecuteCommand(ICommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        command.Execute();
        _undoStack.Push(command);
        ClearStack(_redoStack);
        OnHistoryChanged();
    }

    /// <summary>
    /// Reverts the most recent command, if available.
    /// </summary>
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

    /// <summary>
    /// Re-executes the most recent undone command, if available.
    /// </summary>
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

    /// <summary>
    /// Removes all commands from both history stacks.
    /// </summary>
    public void ClearHistory()
    {
        ClearStack(_undoStack);
        ClearStack(_redoStack);
        OnHistoryChanged();
    }

    /// <summary>
    /// Releases disposable command state owned by the manager.
    /// </summary>
    public void Dispose()
    {
        ClearHistory();
    }

    /// <summary>
    /// Clears a stack and disposes commands that own resources.
    /// </summary>
    /// <param name="stack">The stack to clear.</param>
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

    /// <summary>
    /// Notifies subscribers that the history state changed.
    /// </summary>
    private void OnHistoryChanged()
    {
        HistoryChanged?.Invoke(this, EventArgs.Empty);
    }
}
