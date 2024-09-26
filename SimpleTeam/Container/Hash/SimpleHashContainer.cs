using SimpleTeam.Constant;

namespace SimpleTeam.Container.Hash
{
    public class SimpleHashContainer : SimpleContainer
    {
        public const int MAX_COUNT_OF_SUBNODE = 256;

        //Max level.
        private int maxLevel;
        //Entry of root node of a tree.
        private IHashNode entry;

        public SimpleHashContainer()
        {
            //Set size.
            SetSize(1);
            //Create root node of a tree.
            entry = new SimpleHashElement(Prime.COPRIMES[0], string.Empty);
        }

        public SimpleHashContainer(int capacity)
            : base(capacity)
        {
            //Create root node of a tree.
            entry = new SimpleHashElement(Prime.COPRIMES[0], string.Empty);
        }

        public int GetMaxLevel()
        {
            //Return max level.
            return maxLevel;
        }

        public bool Contains(string key)
        {
            //Level for search.
            int level = 0;
#if DEBUG
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("invalid key(" + key + ")");
            }
#endif
            //Get root.
            IHashNode? node = entry;
            //Do while.
            while (node != null)
            {
                //Get key.
                string nodeKey = node.GetKey();
                //Check result.
                if (!string.IsNullOrEmpty(nodeKey) && nodeKey.Equals(key)) return true;

                //Get index.
                int index = Prime.GetRemainder(key, Prime.COPRIMES[level]);
                //Check index.
                if (index < 0)
                {
                    throw new Exception("index(" + index + ") underflow");
                }
                //Check level.
                if (index > MAX_COUNT_OF_SUBNODE)
                {
                    throw new Exception("index(" + index + ") overflow");
                }
                //Get subnode.
                node = node.GetNode(index);

                //Add level.
                level ++;
            }
            //Return false.
            return false;
        }

        public object? Get(string key)
        {
            //Level for search.
            int level = 0;
#if DEBUG
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("invalid key(" + key + ")");
            }
#endif

            //Get root.
            IHashNode? node = entry;
            //Do while.
            while (node != null)
            {
                //Get key.
                string nodeKey = node.GetKey();
                //Check result.
                if (!string.IsNullOrEmpty(nodeKey) && nodeKey.Equals(key)) return node.GetValue();

                //Get index.
                int index = Prime.GetRemainder(key, Prime.COPRIMES[level]);
                //Check index.
                if (index < 0)
                {
                    throw new Exception("index(" + index + ") underflow");
                }
                //Check level.
                if (index > MAX_COUNT_OF_SUBNODE)
                {
                    throw new Exception("index(" + index + ") overflow");
                }
                //Get subnode.
                node = node.GetNode(index);

                //Add level.
                level ++;
            }
            //Return null.
            return null;
        }

        public object? Put(string key, object? value)
        {
            //Level for search.
            int level = 0;
#if DEBUG
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("invalid key(" + key + ")");
            }
#endif

            //Empty node.
            IHashNode? emptyNode = null;

            //Get root.
            IHashNode node = entry;
            //Do while.
            while (node != null)
            {
                //Get key.
                string nodeKey = node.GetKey();
                //Check result.
                if (string.IsNullOrEmpty(nodeKey))
                {
                    //Set empty node.
                    emptyNode = node;
                }
                //Check node key.
                else if (nodeKey.Equals(key))
                {
                    //Get value.
                    object? oldValue = node.GetValue();
                    //Set object.
                    node.SetValue(value);
                    //Return old value.
                    return oldValue;
                }

                //Get index.
                int index = Prime.GetRemainder(key, Prime.COPRIMES[level]);
                //Check index.
                if (index < 0)
                {
                    throw new Exception("index(" + index + ") underflow");
                }
                //Check level.
                if (index > MAX_COUNT_OF_SUBNODE)
                {
                    throw new Exception("index(" + index + ") overflow");
                }
                //Get subnode.
                IHashNode? subnode = node.GetNode(index);
                //Check node.
                if (subnode == null)
                {
                    //Check empty node.
                    if (emptyNode != null)
                    {
                        //Set key.
                        emptyNode.SetKey(key);
                        //Set object.
                        emptyNode.SetValue(value);
                    }
                    else
                    {
                        //Get size.
                        int size = Prime.COPRIMES[level + 1];
                        //Set subnode.
                        node.SetNode(index, new SimpleHashElement(size, key, value));
                    }
                    //Increase count.
                    IncreaseCount();

                    //Check level.
                    maxLevel = level > maxLevel ? level : maxLevel;
                    //Return null.
                    return null;
                }
                //Set node.
                node = subnode;

                //Add level.
                level ++;
            }
            //Return null.
            return null;
        }

        public object? Remove(string key)
        {
            //Level for search.
            int level = 0;
            //Index for search.
            int index = -1;
#if DEBUG
            if (Empty.IsNullOrEmpty(key))
            {
                throw new ArgumentException("invalid key(" + key + ")");
            }
#endif

            //Parent node.
            IHashNode? parentNode = null;

            //Get root.
            IHashNode? node = entry;
            //Do while.
            while (node != null)
            {
                //Get key.
                string nodeKey = node.GetKey();
                //Check result.
                if (nodeKey.Equals(key))
                {
                    //Get value.
                    object? value = node.GetValue();
                    //Clear value.
                    node.ClearValue();
                    //Clear key.
                    node.SetKey(string.Empty);
                    //Check child.
                    if (!node.HasChild() && parentNode != null)
                    {
                        //Clear this node.
                        parentNode.SetNode(index, null);
                    }
                    //Decrease count.
                    DecreaseCount();
                    //Return value.
                    return value;
                }

                //Get index.
                index = Prime.GetRemainder(key, Prime.COPRIMES[level]);
                //Check index.
                if (index < 0)
                {
                    throw new Exception("index(" + index + ") underflow");
                }
                //Check level.
                if (index > MAX_COUNT_OF_SUBNODE)
                {
                    throw new Exception("index(" + index + ") overflow");
                }
                //Set parent node.
                parentNode = node;
                //Get subnode.
                node = node.GetNode(index);

                //Add level.
                level ++;
            }
            //Return null.
            return null;
        }

        public new void ClearAll()
        {
            base.ClearAll();

            //Clear entry.
            entry = new SimpleHashElement(Prime.COPRIMES[0], string.Empty);
        }
    }
}
