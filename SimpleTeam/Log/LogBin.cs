namespace SimpleTeam.Log
{
    public class LogBin
        : RecycleBin
    {
        //Default Capacity
        public const int DEFAULT_CAPACITY = 4;
        //Max String Length
        public const int MAX_STRING_LENGTH = 1024;

        //String Bin
        private static LogBin logBin;

        //Initialize.
        static LogBin()
        {
            //Create string bin.
            logBin = new LogBin();
            //Set capacity.
            logBin.SetCapacity(DEFAULT_CAPACITY);
        }

        internal override IRecycle CreateObject()
        {
            //Return result.
            return new LogString().SetRecyclable(true);
        }

        public static int GetBinSize()
        {
            //Return result.
            return logBin.GetSize();
        }

        public static int GetBinCapacity()
        {
            //Return capacity.
            return logBin.GetCapacity();
        }

        public static long GetBinCounter(int type)
        {
            //Return counter.
            return logBin.GetCounter(type);
        }

        public static LogString Malloc()
        {
            //Return result.
            return (LogString)logBin.MallocObject();
        }

        public static string Recycle(LogString recycled)
        {
            //Get value.
            string value = recycled.ToString();
            //Check length.
            if (recycled.Length()
                > MAX_STRING_LENGTH)
            {
                //Release this string.
                logBin.IncreaseReleased();
            }
            else
            {
                //Recycle string.
                logBin.RecycleObject(recycled);
            }
            //Return result.
            return value;
        }

        public static void Dump()
        {
            Console.WriteLine("LogBin.Dump : show properties !");
            Console.WriteLine("\tCapacity = {0}", logBin.GetCapacity());
            Console.WriteLine("\tSize = {0}", logBin.GetSize());
            Console.WriteLine("\tCreated Counter = {0}", logBin.GetCounter(CREATED));
            Console.WriteLine("\tReleased Counter = {0}", logBin.GetCounter(RELEASED));
            Console.WriteLine("\tRecycled Counter = {0}", logBin.GetCounter(RECYCLED));
            Console.WriteLine("\tRecreated Counter = {0}", logBin.GetCounter(RECREATED));
        }
    }
}
