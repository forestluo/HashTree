//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Decimal
    {
        public static readonly char[] VALUES = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static bool IsDecimalChar(int value)
        {
            //Check value.
            return value >= '0' && value <= '9';
        }

        public static bool IsDecimalString(string? value)
        {
            //Check string.
            if(string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check decimal.
                if (!IsDecimalChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
