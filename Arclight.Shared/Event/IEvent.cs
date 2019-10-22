namespace Arclight.Shared.Event
{
    public interface IEvent
    {
        bool CanExecute();
        void Execute();
    }
}
