//MMS-WAP
namespace SimpleTeam.Constant
{
    public class LowerAlpha
    {
        public static readonly char[] VALUES = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public static bool IsLAChar(char value)
        {
            //Check value.
            return value >= 'a' && value <= 'z';
        }

        public static bool IsLAString(string? value)
        {
            //Check string.
            if(string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check lower alpha.
                if (!IsLAChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
