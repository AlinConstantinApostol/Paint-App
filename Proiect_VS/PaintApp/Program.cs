/*************************************************************************
 *                                                                       *
 * File:          Program.cs                                             *
 * Copyright:     (c) 2026, Vrinceanu Sterica                            *
 * E-mail:        sterica.vrinceanu@student.tuiasi.ro                    *
 * Description:   Initializes the program.                               *
 *                                                                       *
 * This code and information is provided "as is" without warranty of     *
 * any kind, either expressed or implied, including but not limited      *
 * to the implied warranties of merchantability or fitness for a         *
 * particular purpose. You are free to use this source code in your      *
 * applications as long as the original copyright notice is included.    *
 *                                                                       *
 *************************************************************************/

using System.Threading;

namespace PaintApp;

static class Program
{
    /// <summary>
    /// Starts the WinForms application and registers global exception handlers.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += HandleThreadException;
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

        // The main form owns the application workflow and the drawing surface.
        Application.Run(new MainCanvasForm());
    }

    /// <summary>
    /// Displays a controlled error message for exceptions raised on the UI thread.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">Exception data from the UI thread.</param>
    private static void HandleThreadException(object? sender, ThreadExceptionEventArgs e)
    {
        ShowFatalError(e.Exception);
    }

    /// <summary>
    /// Displays a controlled error message for non-UI unhandled exceptions.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">Unhandled exception metadata.</param>
    private static void HandleUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception ?? new Exception("Unknown unhandled exception.");
        ShowFatalError(exception);
    }

    /// <summary>
    /// Shows a final fallback message so the application fails gracefully.
    /// </summary>
    /// <param name="exception">The exception that reached the top-level handler.</param>
    private static void ShowFatalError(Exception exception)
    {
        MessageBox.Show(
            $"A aparut o eroare neasteptata:\n{exception.Message}",
            "Eroare critica",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
