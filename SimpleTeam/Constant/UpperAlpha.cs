//MMS-WAP
namespace SimpleTeam.Constant
{
    public class UpperAlpha
    {
        //Upper Alpha
        public static readonly char[] VALUES = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'G', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public static bool IsUAChar(int value)
        {
            //Check value.
            return value >= 'A' && value <= 'Z';
        }

        public static bool IsUAString(string? value)
        {
            //Check string.
            if(string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check upper alpha.
                if (!IsUAChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
