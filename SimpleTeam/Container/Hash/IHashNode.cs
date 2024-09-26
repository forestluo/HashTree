namespace SimpleTeam.Container.Hash
{
    public interface IHashNode : INode
    {
        public string GetKey();

        public void SetKey(string key);

        public int NodeCount();

        public bool HasChild();

        public IHashNode? GetNode(int index);

        public void SetNode(int index, IHashNode? node);
    }
}
