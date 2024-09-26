namespace SimpleTeam.Container.File
{
    internal class SafelyClosed
    {
        //Closed
        public const int CLOSED = 0;
        //Opened
        public const int OPENED = 1;

        public static bool IsValid(int value)
        {
            //Return result.
            return value >= 0 && value <= 1;
        }
    }
}
