namespace SimpleTeam.Function
{
    public class DecimalFormat
    {
        public static int ParseValue(char value)
        {
            //Check value.
            return (value >= '0' && value <= '9') ? (value - '0') : -1;
        }

        public static byte ParseByte(string decimals)
        {
            //Value.
            byte value = 0;
            //Get Decimals.
            for (int i = 0; decimals != null && i < decimals.Length; i++)
            {
                //Multiply 10.
                value *= 10;
                //Add value.
                value += (byte)(decimals[i] - '0');
            }
            //Return value.
            return value;
        }

        public static short ParseShort(string decimals)
        {
            //Value.
            short value = 0;
            //Get Decimals.
            for (int i = 0; decimals != null && i < decimals.Length; i++)
            {
                //Multiply 10.
                value *= 10;
                //Add value.
                value += (short)(decimals[i] - '0');
            }
            //Return value.
            return value;
        }

        public static int ParseInteger(string decimals)
        {
            //Value.
            int value = 0;
            //Get Decimals.
            for (int i = 0; decimals != null && i < decimals.Length; i++)
            {
                //Multiply 10.
                value *= 10;
                //Add value.
                value += (int)(decimals[i] - '0');
            }
            //Return value.
            return value;
        }

        public static long ParseLong(string decimals)
        {
            //Value.
            long value = 0;
            //Get Decimals.
            for (int i = 0; decimals != null && i < decimals.Length; i++)
            {
                //Multiply 10.
                value *= 10;
                //Add value.
                value += (long)(decimals[i] - '0');
            }
            //Return value.
            return value;
        }
    }
}
