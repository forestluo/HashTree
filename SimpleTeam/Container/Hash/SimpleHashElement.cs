using SimpleTeam.Constant;

namespace SimpleTeam.Container.Hash
{
    internal class SimpleHashElement :
        KeyElement, IHashNode
    {
        //Subnodes
        private IHashNode?[] subnodes;

        public SimpleHashElement(int size, string key)
            : base(key, size)
        {
#if DEBUG
            if (size < 0 || size > SimpleHashContainer.MAX_COUNT_OF_SUBNODE)
            {
                throw new ArgumentException("invalid size(" + size + ")");
            }
#endif
            //Create subnodes.
            subnodes = new IHashNode[size];
        }

        public SimpleHashElement(int size, string key, object? value)
            : base(key, value)
        {
#if DEBUG
            if(Empty.IsNullOrEmpty(key))
            {
                throw new ArgumentException("invalid key(" + key + ")");
            }
            if (size < 0 || size > SimpleHashContainer.MAX_COUNT_OF_SUBNODE)
            {
                throw new ArgumentException("invalid size(" + size + ")");
            }
#endif
            //Create subnodes.
            subnodes = new IHashNode[size];
        }

        public int NodeCount()
        {
            //Return count.
            return subnodes.Length;
        }

        public bool HasChild()
        {
            //Return result.
            return !subnodes.All(subnode => (subnode == null));
        }

        public IHashNode? GetNode(int index)
        {
#if DEBUG
            if (index < 0 || index > SimpleHashContainer.MAX_COUNT_OF_SUBNODE)
            {
                throw new ArgumentException("invalid index(" + index + ")");
            }
#endif
            //Return subnode.
            return subnodes[index];
        }

        public void SetNode(int index, IHashNode? node)
        {
#if DEBUG
            if (index < 0 || index > SimpleHashContainer.MAX_COUNT_OF_SUBNODE)
            {
                throw new ArgumentException("invalid index(" + index + ")");
            }
#endif
            //Set subnode.
            subnodes[index] = node;
        }
    }
}
