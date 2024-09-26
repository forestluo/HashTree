//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Token
    {
        public static bool IsTokenChar(int value)
        {
            //Any character.
            return Ascii.IsAsciiChar(value) && !Control.IsControlChar(value);
        }

        public static bool IsTokenString(string? value)
        {
            //Check value.
            if(string.IsNullOrEmpty(value)) return false;
            //Check value.
            for (int i = 0; i < value.Length; i++)
            {
                //Check token.
                if (!IsTokenChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
