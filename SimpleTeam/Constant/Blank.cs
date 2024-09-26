//MMS-WAP
namespace SimpleTeam.Constant
{
    public class Blank
    {
        public static bool IsBlankChar(int value)
        {
            //Check space.
            return BS.IsBSChar(value) || CR.IsCRChar(value)
                || LF.IsLFChar(value) || HT.IsHTChar(value);
        }

        public static bool IsBlankString(string? value)
        {
            //Check string.
            if (string.IsNullOrEmpty(value)) return false;
            //Check value.
            for (int i = 0; i < value.Length; i++)
            {
                //Check char.
                if (!IsBlankChar(value[i])) return false;
            }
            //Return true.
            return true;
        }
    }
}
