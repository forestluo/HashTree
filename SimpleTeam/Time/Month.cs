namespace SimpleTeam.Time
{
    public class Month
    {
        //Value.
        public const long VALUE = 30 * Day.VALUE;

        //Get month of today
        public static int GetMonth(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Month;
        }
    }
}
