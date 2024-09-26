using SimpleTeam.Container;

namespace SimpleTeam.Log
{
    public abstract class RecycleBin : IContainer
    {
        //Created Count
        public const int CREATED = 0;
        //Released Count
        public const int RELEASED = 1;
        //Recycled Count
        public const int RECYCLED = 2;
        //Recreated Count
        public const int RECREATED = 3;

        //Counters.
        private long[] counters;

        //Capacity.
        private int capacity;
        //Recycled objects.
        private List<IRecycle> objects;

        public RecycleBin()
        {
            //Set capacity.
            capacity = IContainer.WITHOUT_LIMIT;
            //Create objects.
            objects = new List<IRecycle>();

            //Create counters.
            counters = new long[4];
            //Initialize counters.
            for (int i = 0; i < counters.Length; i ++) counters[i] = 0;
        }

        ~RecycleBin()
        {
            //Clear objects.
            objects.Clear();
        }

        public int GetSize()
        {
            lock (this)
            {
                //Return result.
                return objects.Count;
            }
        }

        public int GetCapacity()
        {
            lock (this)
            {
                //Return capacity.
                return capacity;
            }
        }

        public void SetCapacity(int capacity)
        {
            lock (this)
            {
                //Set capacity.
                this.capacity = capacity;
            }
        }

        public bool IsFull()
        {
            lock (this)
            {
                //Check capacity.
                if (capacity < 0) return false;
                //Return result.
                return objects.Count >= capacity;
            }
        }

        public bool IsEmpty()
        {
            lock (this)
            {
                //Return result.
                return objects.Count <= 0;
            }
        }

        public long GetCounter(int type)
        {
            lock (this)
            {
                //Check type.
                switch (type)
                {
                    case CREATED:
                        return counters[CREATED];
                    case RELEASED:
                        return counters[RELEASED];
                    case RECYCLED:
                        return counters[RECYCLED];
                    case RECREATED:
                        return counters[RECREATED];
                }
            }
            //Return -1.
            return -1;
        }

        public void Clear()
        {
            lock (this)
            {
                //Clear.
                objects.Clear();
                //Initialize counters.
                for (int i = 0; i < counters.Length; i++) counters[i] = 0;
            }
        }

        protected abstract
            IRecycle CreateObject();

        protected void IncreaseReleased()
        {
            lock (this)
            {
                counters[RELEASED] ++;
            }
        }

        protected IRecycle MallocObject()
        {
            lock (this)
            {
                //Check empty.
                if (objects.Count <= 0)
                {
                    //Increase new count.
                    counters[CREATED] ++;
                    //Return result.
                    return CreateObject();
                }
                //Increase recreated count.
                counters[RECREATED] ++;
                //Get element
                IRecycle recycled = objects.ElementAt(0);
                //Remove element
                objects.RemoveAt(0);
                //Return result.
                return recycled;
            }
        }

        protected void RecycleObject(IRecycle recycled)
        {
            //Clear.
            recycled.Clear();
            //Lock this.
            lock (this)
            {
                //Keep only a reasonable count in the memory.
                if (capacity > 0 &&
                    objects.Count >= capacity)
                {
                    //Discard element.
                    //Increase released count.
                    counters[RELEASED] ++;
                }
                else
                {
                    //Add object to tail.
                    objects.Add(recycled); counters[RECYCLED] ++;
                }
            }
        }
    }
}

