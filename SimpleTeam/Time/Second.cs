namespace SimpleTeam.Time
{
    public class Second
    {
        //Value in milliseconds.
        public const long VALUE = 1000 * Millisecond.VALUE;

        //Current time in second
        public static long CurrentTime()
        {
            //Return result.
            return Millisecond.CurrentTime() / VALUE;
        }

        //Get second of today
        public static int GetSecondOfDay(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Second;
        }
    }
}
