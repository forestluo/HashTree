//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Separator
    {
        //Separators.
        public static readonly char[] VALUES = new char[] { '(', ')', '<', '>', '@', ',', ';', '\\', '"', '/', '[', ']', '?', '=', '{', '}', ' ', '\t' };

        public static bool IsSeparatorChar(int value)
        {
            //Check values.
            for (int i = 0; i < VALUES.Length; i++)
            {
                //Check value.
                if (value == VALUES[i]) return true;
            }
            //Return false.
            return false;
        }

        public static string[]? SeparateString(string? value)
        {
            //Check value.
            if (string.IsNullOrEmpty(value)) return null;
            //Return result.
            return value.Split(VALUES);
        }

        public static string[]? SeparateString(string? value, string seprators)
        {
            //Check value.
            if (string.IsNullOrEmpty(value)) return null;
            //Create parts.
            return value.Split(seprators.ToCharArray());
        }
    }
}
