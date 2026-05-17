// ICommand.cs - command contract used by the undo/redo history manager.

namespace StateManager;

/// <summary>
/// Defines the actions required by commands stored in the history manager.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Applies the command effect.
    /// </summary>
    void Execute();

    /// <summary>
    /// Reverts the command effect.
    /// </summary>
    void Undo();
}
