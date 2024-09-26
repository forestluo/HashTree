namespace SimpleTeam.Time
{
    public class Millisecond
    {
        //Value.
        public const long VALUE = 1;
        //One milliseconds in ticks
        public const long TICKS = 10000;

        //Current time in milliseconds
        public static long CurrentTime()
        {
            //Return result.
            return DateTime.Now.Ticks / TICKS;
        }

        //Get millisecond of today
        public static int GetMillisecondOfDay(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Millisecond;
        }
    }
}
