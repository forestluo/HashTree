namespace SimpleTeam.Time
{
    public class Year
    {
        //Value.
        public const long VALUE = 365 * Day.VALUE;

        //Get year of today
        public static int GetYear(long ticks)
        {
            //Create datetime.
            return new DateTime(ticks).Year;
        }
    }
}
