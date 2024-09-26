namespace SimpleTeam.Container.Hash
{
    internal interface IHashNode
        : INode
    {
        public string GetKey();

        public void SetKey(string key);

        public int NodeCount();

        public bool HasChild();

        public IHashNode? GetNode(int index);

        public void SetNode(int index, IHashNode? node);
    }
}
