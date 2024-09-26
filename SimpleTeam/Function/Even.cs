namespace SimpleTeam.Function
{
    public class Even
    {
        public static bool IsEven(int value)
        {
            //Check last bit.
            return (value & 0x01) == 0;
        }
    }
}
