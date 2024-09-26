using SimpleTeam.Constant;

namespace SimpleTeam.Function
{
    public class Comparer
    {
        public static int Compare(byte[]? b1, byte[]? b2)
        {
            //Get length1.
            int length1 = b1 == null ? 0 : b1.Length;
            //Get length2.
            int length2 = b2 == null ? 0 : b2.Length;
            //Check result.
            if (length1 == length2 && length1 > 0)
            {
                //Check bytes.
                for (int i = 0; i < length1; i++)
                {
#pragma warning disable CS8602
                    //Get result.
                    int result = Sign.GetSign((b1[i] & 0xff) - (b2[i] & 0xff));
#pragma warning restore CS8602
                    //Check result.
                    if (result != Sign.ZERO) return result;
                }
                //Return zero.
                return Sign.ZERO;
            }
            //Return.
            return Sign.GetSign(length1 - length2);
        }

        public static int Compare(string? b1, string? b2)
        {
            //Get length1.
            int length1 = b1 == null ? 0 : b1.Length;
            //Get length2.
            int length2 = b2 == null ? 0 : b2.Length;
            //Check result.
            if (length1 == length2 && length1 > 0)
            {
                //Check bytes.
                for (int i = 0; i < length1; i++)
                {
#pragma warning disable CS8602
                    //Get result.
                    int result = Sign.GetSign(b1[i] - b2[i]);
#pragma warning restore CS8602
                    //Check result.
                    if (result != Sign.ZERO) return result;
                }
                //Return zero.
                return Sign.ZERO;
            }
            //Return.
            return Sign.GetSign(length1 - length2);
        }

        public static bool Equal(string? s1, string? s2, bool nullable)
        {
            //Check s1.
            if (s1 != null && s1.Length > 0)
            {
                //Check s2.
                if (s2 == null || s2.Length <= 0) return false;
                //Compare s2.
                return s2.Equals(s1);
            }
            //Return result.
            return nullable && (s2 == null || s2.Length <= 0);
        }

        public static bool EqualIgnoreCase(string? s1, string? s2, bool nullable)
        {
            //Check s1.
            if (s1 != null && s1.Length > 0)
            {
                //Check s2.
                if (s2 == null || s2.Length <= 0) return false;
                //Compare s2.
                return s2.Equals(s1, StringComparison.CurrentCultureIgnoreCase);
            }
            //Return result.
            return nullable && (s2 == null || s2.Length <= 0);
        }

        public static bool Equal(byte[]? b1, byte[]? b2, bool nullable)
        {
            //Check b1.
            if (b1 != null && b1.Length > 0)
            {
                //Check b2.
                if (b2 == null || b2.Length <= 0) return false;
                //Check bytes.
                for (int i = 0; i < b1.Length && i < b2.Length; i++)
                {
                    //Check bytes.
                    if (b1[i] != b2[i]) return false;
                }
                //Return length.
                return b1.Length == b2.Length;
            }
            //Return result.
            return nullable && (b2 == null || b2.Length <= 0);
        }

        public static bool Equal(byte[]? b1, byte[]? b2, int length, bool nullable)
        {
            //Check b1.
            if (b1 != null && b1.Length > 0)
            {
                //Check b2.
                if (b2 == null || b2.Length <= 0) return false;
                //Check bytes.
                for (int i = 0; i < length && i < b1.Length && i < b2.Length; i++)
                {
                    //Check bytes.
                    if (b1[i] != b2[i]) return false;
                }
                //Check length.
                if (length <= b1.Length && length <= b2.Length)
                {
                    //Return result.
                    return length > 0 || nullable;
                }
                //Return length.
                return length >= b1.Length && length >= b2.Length ? b1.Length == b2.Length : false;
            }
            //Return result.
            return nullable && (b2 == null || b2.Length <= 0);
        }
    }
}
