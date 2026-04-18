namespace StateManager;

public interface ICommand
{
    void Execute();

    void Undo();
}
