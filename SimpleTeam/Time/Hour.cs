namespace SimpleTeam.Time
{
    public class Hour
    {
        //Value.
        public const long VALUE = 60 * Minute.VALUE;

        //Current time in hours
        public static long CurrentTime()
        {
            //Return result.
            return Millisecond.CurrentTime() / VALUE;
        }

        //Get hour of today
        public static int GetHourOfDay(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Hour;
        }
    }
}
