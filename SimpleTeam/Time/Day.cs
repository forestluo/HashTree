namespace SimpleTeam.Time
{
    public class Day
    {
        //Value.
        public const long VALUE = 24 * Hour.VALUE;

        //Current in days
        public static long CurrentTime()
        {
            //Return result.
            return Millisecond.CurrentTime() / VALUE;
        }

        //Get day of today
        public static int GetDay(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Day;
        }
    }
}
