namespace SimpleTeam.Constant
{
    public class Empty
    {
        public const string STRING = "";
        public static readonly byte[] BYTES = [];

        public static bool IsNullOrEmpty(string? value)
        {
            //Return result.
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrEmpty(byte[]? value)
        {
            //Return result.
            return value == null || value.Length <= 0;
        }
    }
}
