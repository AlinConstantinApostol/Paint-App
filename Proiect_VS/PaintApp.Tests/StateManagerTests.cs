using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateManager;

namespace PaintApp.Tests;

/// <summary>
/// MSTest suite for DrawCommand and CommandManager.
/// </summary>
[TestClass]
public class StateManagerTests
{
    [TestMethod]
    public void DrawCommand_Execute_AppliesNewState()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var command = new DrawCommand(previousState, newState, canvasHost);

        command.Execute();

        Assert.AreEqual(Color.Blue.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
        Assert.AreEqual(1, canvasHost.SetCalls);
    }

    [TestMethod]
    public void DrawCommand_Undo_AppliesPreviousState()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var command = new DrawCommand(previousState, newState, canvasHost);

        command.Undo();

        Assert.AreEqual(Color.Red.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }

    [TestMethod]
    public void DrawCommand_Execute_UsesClonedBitmapState()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var command = new DrawCommand(previousState, newState, canvasHost);

        using (var graphics = Graphics.FromImage(newState))
        {
            graphics.Clear(Color.Green);
        }

        command.Execute();

        Assert.AreEqual(Color.Blue.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }

    [TestMethod]
    public void DrawCommand_Constructor_NullCanvas_Throws()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);

        Assert.ThrowsException<ArgumentNullException>(() => _ = new DrawCommand(previousState, newState, null!));
    }

    [TestMethod]
    public void CommandManager_ExecuteCommand_EnablesUndo()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(previousState, newState, canvasHost));

        Assert.IsTrue(manager.CanUndo);
    }

    [TestMethod]
    public void CommandManager_ExecuteCommand_Null_Throws()
    {
        using var manager = new CommandManager();

        Assert.ThrowsException<ArgumentNullException>(() => manager.ExecuteCommand(null!));
    }

    [TestMethod]
    public void CommandManager_Undo_EnablesRedo()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(previousState, newState, canvasHost));
        manager.Undo();

        Assert.IsTrue(manager.CanRedo);
        Assert.AreEqual(Color.Red.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }

    [TestMethod]
    public void CommandManager_Redo_ReappliesNewestState()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(previousState, newState, canvasHost));
        manager.Undo();
        manager.Redo();

        Assert.AreEqual(Color.Blue.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }

    [TestMethod]
    public void CommandManager_ExecuteCommand_ClearsRedoStack()
    {
        using var stateA = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var stateB = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var stateC = TestBitmapFactory.CreateSolidBitmap(Color.Green);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(stateA, stateB, canvasHost));
        manager.Undo();
        manager.ExecuteCommand(new DrawCommand(stateB, stateC, canvasHost));

        Assert.IsFalse(manager.CanRedo);
    }

    [TestMethod]
    public void CommandManager_ClearHistory_DisablesUndoAndRedo()
    {
        using var previousState = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var newState = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(previousState, newState, canvasHost));
        manager.Undo();
        manager.ClearHistory();

        Assert.IsFalse(manager.CanUndo);
        Assert.IsFalse(manager.CanRedo);
    }

    [TestMethod]
    public void CommandManager_MultipleUndo_RestoresOldestState()
    {
        using var stateA = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var stateB = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var stateC = TestBitmapFactory.CreateSolidBitmap(Color.Green);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(stateA, stateB, canvasHost));
        manager.ExecuteCommand(new DrawCommand(stateB, stateC, canvasHost));
        manager.Undo();
        manager.Undo();

        Assert.AreEqual(Color.Red.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }

    [TestMethod]
    public void CommandManager_MultipleRedo_ReappliesLatestState()
    {
        using var stateA = TestBitmapFactory.CreateSolidBitmap(Color.Red);
        using var stateB = TestBitmapFactory.CreateSolidBitmap(Color.Blue);
        using var stateC = TestBitmapFactory.CreateSolidBitmap(Color.Green);
        using var canvasHost = new TestCanvasHost();
        using var manager = new CommandManager();

        manager.ExecuteCommand(new DrawCommand(stateA, stateB, canvasHost));
        manager.ExecuteCommand(new DrawCommand(stateB, stateC, canvasHost));
        manager.Undo();
        manager.Undo();
        manager.Redo();
        manager.Redo();

        Assert.AreEqual(Color.Green.ToArgb(), TestBitmapFactory.ReadCenterPixel(canvasHost.CurrentImage!).ToArgb());
    }
}
