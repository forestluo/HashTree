//MMS-WAP
namespace SimpleTeam.Constant
{
    public class LF
    {
        //Value
        public const int VALUE = 10;
        //Char Value
        public const char CHAR = '\n';

        public static bool IsLFChar(int value)
        {
            //Check value.
            return value == VALUE;
        }
    }
}
