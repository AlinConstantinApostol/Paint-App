/*************************************************************************
 *                                                                       *
 * File:          ICommand.cs                                            *
 * Copyright:     (c) 2026, Apostol Alin-Constantin                      *
 * E-mail:        alin-constantin.apostol@student.tuiasi.ro              *
 * Description:   Defines the ICommand interface for commands            *
 *                in the history manager.                                *
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
