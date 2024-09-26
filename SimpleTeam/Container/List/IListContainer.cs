namespace SimpleTeam.Container.List
{
    public interface IListContainer
    {
        public object? GetHead();

        public object? GetTail();

        public object? RemoveHead();

        public object? RemoveTail();

        public void AddHead(object? value);

        public void AddTail(object? value);
    }
}
