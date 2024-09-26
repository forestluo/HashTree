namespace SimpleTeam.Constant
{
    public class Hex
    {
        //Hex.
        public static readonly char[] VALUES = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        public static bool IsHexChar(int value)
        {
            //Check value.
            if (value >= 'A' && value <= 'F')
            {
                //Return true.
                return true;
            }
            //Check value.
            if (value >= 'a' && value <= 'f')
            {
                //Return true.
                return true;
            }
            //Check decimal.
            return Decimal.IsDecimalChar(value);
        }

        public static bool IsHexString(string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            //Check string.
            for (int i = 0; i < value.Length; i++)
            {
                //Check hex.
                if (!IsHexChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
