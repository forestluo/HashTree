//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Octet
    {
        public static bool IsOctetChar(int value)
        {
            //Check value.
            return value >= 0 && value <= 255;
        }
    }
}
