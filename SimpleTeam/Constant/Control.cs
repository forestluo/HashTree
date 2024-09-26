namespace SimpleTeam.Constant
{
    public class Control
    {
        public static bool IsControlChar(int value)
        {
            //Check value.
            return (value >= 0 && value <= 31) || value == 127;
        }
    }
}
