//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Ascii
    {
        public static bool IsAsciiChar(int value)
        {
            //Return result.
            return value >= 0 && value <= 127;
        }

        public static bool IsVisibleChar(int value)
        {
            //Return result.
            return value >= 32 && value < 127;
        }

        public static bool IsInvisibleChar(int value)
        {
            //Return result.
            return (value >= 0 && value < 32) || value == 127;
        }

        public static bool IsAsciiString(string? value)
        {
            //Check string.
            if(string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check char.
                if (!IsAsciiChar(value[i])) return false;
            }
            //Return true.
            return true;
        }

        public static bool IsVisibleString(string? value)
        {
            //Check string.
            if (string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check char.
                if (!IsVisibleChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
