//MMS-WAP
namespace SimpleTeam.Constant
{
    public class HT
    {
        //Value
        public const int VALUE = 9;
        //Char Value
        public const char CHAR = '\t';

        public static bool IsHTChar(int value)
        {
            //Check value.
            return value == VALUE;
        }
    }
}
