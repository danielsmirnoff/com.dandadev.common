namespace CommonDan
{
    public interface ICommand {
        void Execute();
        void Undo();
    }
}