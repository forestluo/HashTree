namespace SimpleTeam.Container.List
{
    public interface IListNode : INode
    {
        public IListNode GetNext();

        public IListNode GetPrevious();

        public void SetNext(IListNode next);

        public void SetPrevious(IListNode previous);

        public void SetNextPrevious(IListNode previous);

        public void SetPreviousNext(IListNode next);
    }
}
