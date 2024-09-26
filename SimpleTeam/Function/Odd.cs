namespace SimpleTeam.Function
{
    public class Odd
    {
        public static bool IsOdd(int value)
        {
            //Check last bit.
            return (value & 0x01) == 1;
        }
    }
}
