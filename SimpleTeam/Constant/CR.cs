//MMS-WAP
namespace SimpleTeam.Constant
{
    public class CR
    {
        //Value
        public const int VALUE = 13;
        //Char Value
        public const char CHAR = '\r';

        public static bool IsCRChar(int value)
        {
            //Check value.
            return value == VALUE;
        }
    }
}
