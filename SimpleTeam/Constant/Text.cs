//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Text
    {
        public static bool IsTextChar(int value)
        {
            //Any octet.
            return Octet.IsOctetChar(value) && !Control.IsControlChar(value);
        }

        public static bool IsTextString(string? value)
        {
            //Check value.
            if(string.IsNullOrEmpty(value)) return false;
            //Check value.
            for (int i = 0; i < value.Length; i++)
            {
                //Check text.
                if (!IsTextChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
