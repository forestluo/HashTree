namespace SimpleTeam.Container
{
    public interface IContainer
    {
        public const int WITHOUT_LIMIT = -1;

        public int GetSize();

        public int GetCapacity();

        public void SetCapacity(int capacity);

        public bool IsFull();

        public bool IsEmpty();
    }
}
