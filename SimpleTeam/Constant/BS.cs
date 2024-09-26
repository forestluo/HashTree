//MMS-WAP
namespace SimpleTeam.Constant
{
    public class BS
    {
        //Value
        public const int VALUE = 32;
        //Char Value
        public const char CHAR = ' ';

        public static bool IsBSChar(int value)
        {
            //Check value.
            return value == VALUE;
        }
    }
}
