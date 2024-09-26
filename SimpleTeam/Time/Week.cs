namespace SimpleTeam.Time
{
    public class Week
    {
        //Value.
        public const long VALUE = 7 * Day.VALUE;

        //Current time in weeks.
        public static long CurrentTime()
        {
            //Return result.
            return Millisecond.CurrentTime() / VALUE;
        }

        //Get week of today
        public static DayOfWeek GetWeek(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).DayOfWeek;
        }
    }
}
