using System.Text;

using SimpleTeam.Constant;

namespace SimpleTeam.Function
{
    public class HexFormat
    {
        public static string ToString(byte value)
        {
            //Return string.
            return new StringBuilder().
                Append(Hex.VALUES[(int)((value >> 4) & 0x0f)]).
                Append(Hex.VALUES[(int)((value >> 0) & 0x0f)]).ToString();
        }

        public static string ToString(short value)
        {
            //Create string buffer.
            StringBuilder buffer = new ();
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.SHORT; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(int)((value >> (4 * (2 * SizeOf.SHORT - i - 1))) & 0x0f)]);
            }
            //Return string.
            return buffer.ToString();
        }

        public static string ToString(int value)
        {
            //Create string buffer.
            StringBuilder buffer = new ();
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.INTEGER; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(int)((value >> (4 * (2 * SizeOf.INTEGER - i - 1))) & 0x0f)]);
            }
            //Return string.
            return buffer.ToString();
        }

        public static string ToString(long value)
        {
            //Create string buffer.
            StringBuilder buffer = new ();
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.LONG; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(int)((value >> (4 * (2 * SizeOf.LONG - i - 1))) & 0x0f)]);
            }
            //Return string.
            return buffer.ToString();
        }

        public static string ToString(byte[]? bytes)
        {
            //Return result.
            return ToString(bytes, 0, bytes != null ? bytes.Length : 0);
        }

        public static string ToString(byte[]? bytes, int length)
        {
            //Return result.
            return ToString(bytes, 0, length);
        }

        public static string ToString(byte[]? bytes, int offset, int length)
        {
            //Get size.
            int size = bytes != null ? bytes.Length : 0;
            //Check result.
            if (size < length) length = size;

            //Create string buffer.
            StringBuilder buffer = new StringBuilder();
            //Convert to hex string.
            for (int i = 0; bytes != null && i < length; i++)
            {
//#pragma warning disable CS8602
                //Append char.
                buffer.Append(Hex.VALUES[(int)((bytes[offset + i] >> 4) & 0x0f)]);
                //Append char.
                buffer.Append(Hex.VALUES[(int)(bytes[offset + i] & 0x0f)]);
//#pragma warning restore CS8602
            }
            //Return result.
            return buffer.ToString();
        }

        public static int ParseValue(char hex)
        {
            //Check value.
            if (hex >= 'A' && hex <= 'F')
            {
                //Return result.
                return hex - 'A' + 10;
            }
            //Check value.
            if (hex >= 'a' && hex <= 'f')
            {
                //Return result.
                return hex - 'a' + 10;
            }
            //Check decimal.
            return DecimalFormat.ParseValue(hex);
        }

        public static byte ParseByte(string? hexes)
        {
            //Value.
            byte value = 0;
            //Get value.
            for (int i = hexes != null && hexes.StartsWith("0x") ? 2 : 0;
                hexes != null && i < hexes.Length; i ++)
            {
                //Shift left.
                value <<= 4;
                //Add value.
                value |= (byte)(ParseValue(hexes[i]) & 0x0f);
            }
            //Return value.
            return value;
        }

        public static short ParseShort(string? hexes)
        {
            //Value.
            short value = 0;
            //Get value.
            for (int i = hexes != null && hexes.StartsWith("0x") ? 2 : 0;
                hexes != null && i < hexes.Length; i ++)
            {
                //Shift left.
                value <<= 4;
                //Add value.
                value |= (short)(ParseValue(hexes[i]) & 0x0f);
            }
            //Return value.
            return value;
        }

        public static int ParseInteger(string? hexes)
        {
            //Value.
            int value = 0;
            //Get value.
            for (int i = hexes != null && hexes.StartsWith("0x") ? 2 : 0;
                hexes != null && i < hexes.Length; i++)
            {
                //Shift value.
                value <<= 4;
                //Add value.
                value |= (int)(ParseValue(hexes[i]) & 0x0f);
            }
            //Return value.
            return value;
        }

        public static long ParseLong(string? hexes)
        {
            //Value.
            long value = 0;
            //Get value.
            for (int i = hexes != null && hexes.StartsWith("0x") ? 2 : 0;
                hexes != null && i < hexes.Length; i++)
            {
                //Shift value.
                value <<= 4;
                //Add value.
                value |= (long)(ParseValue(hexes[i]) & 0x0f);
            }
            //Return value.
            return value;
        }

        public static byte[]? ParseBytes(string? hexes)
        {
            //Check hexes.
            if (string.IsNullOrEmpty(hexes)) return null;
            //Start with prefix.
            bool prefix = hexes.StartsWith("0x");
            //Evenize string length.
            int length = ((hexes.Length - (prefix ? 2 : 0)) + 1) >> 1;
            //Create bytes.
            byte[] bytes = new byte[length];
            //Get bytes.
            for (int i = prefix ? 2 : 0; i < 2 * length; i++)
            {
                //Get value.
                byte value = 0;
                //Check result.
                if (i < hexes.Length) value = (byte)ParseValue(hexes[i]);
                //Add value.
                bytes[i >> 1] |= (byte)(value << (Odd.IsOdd(i) ? 0 : 4));
            }
            //Return bytes.
            return bytes;
        }
    }
}
