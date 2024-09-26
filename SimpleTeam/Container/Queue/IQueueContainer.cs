namespace SimpleTeam.Container.Queue
{
    public interface IQueueContainer
    {
        public object? GetHead();

        public object? RemoveHead();

        public void AddTail(object value);
    }
}
