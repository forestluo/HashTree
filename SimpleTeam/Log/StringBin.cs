namespace SimpleTeam.Log
{
    public class StringBin : RecycleBin
    {
        //Default Capacity
        public const int DEFAULT_CAPACITY = 32;
        //Max String Length
        public const int MAX_STRING_LENGTH = 1024;

        //String Bin
        private static StringBin stringBin;

        //Initialize.
        static StringBin()
        {
            //Create string bin.
            stringBin = new StringBin();
            //Set capacity.
            stringBin.SetCapacity(DEFAULT_CAPACITY);
        }

        protected override IRecycle CreateObject()
        {
            //Return result.
            return new RecycleString();
        }

        public static int GetBinSize()
        {
            //Return result.
            return stringBin.GetSize();
        }

        public static int GetBinCapacity()
        {
            //Return capacity.
            return stringBin.GetCapacity();
        }

        public static long GetBinCounter(int type)
        {
            //Return counter.
            return stringBin.GetCounter(type);
        }

        public static RecycleString Malloc()
        {
            //Return result.
            return (RecycleString)stringBin.MallocObject();
        }

        public static string Recycle(RecycleString recycled)
        {
            //Get value.
            string value = recycled.ToString();
            //Check length.
            if (recycled.Length()
                > MAX_STRING_LENGTH)
            {
                //Discard recycled.
                //Release this string.
                stringBin.IncreaseReleased();
            }
            else
            {
                //Recycle string.
                stringBin.RecycleObject(recycled);
            }
            //Return result.
            return value;
        }
    }
}
