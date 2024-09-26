namespace SimpleTeam.Time
{
    public class Minute
    {
        //Value.
        public const long VALUE = 60 * Second.VALUE;

        //Current time in minutes
        public static long CurrentTime()
        {
            //Return result.
            return Millisecond.CurrentTime() / VALUE;
        }

        //Get minute of today
        public static int GetMinuteOfDay(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Minute;
        }
    }
}
