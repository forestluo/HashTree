namespace SimpleTeam.Container.Queue
{
    internal interface IQueueContainer
    {
        public object? GetHead();

        public object? RemoveHead();

        public void AddTail(object value);
    }
}
