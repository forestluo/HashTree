using SimpleTeam.Constant;

namespace SimpleTeam.Function
{
    public class SimpleHash
    {
        public static long LongHash(byte[] bytes)
        {
            //Hash code.
            long hashCode = 0;
            //Do while.
            for (int i = 0; i < bytes.Length; i++)
            {
                //Get hash code.
                hashCode = (hashCode * 16777619) ^ (bytes[i] & 0xff);
            }
            ////////////////////////////////////////////////////
            //
            //Add effect of length to the hash code.
            //
            //Get length.
            int length = bytes.Length;
            //Do while.
            for (int i = 0; i < SizeOf.INTEGER; i++)
            {
                //Add length as a byte.
                hashCode = (hashCode * 16777619) ^ ((length >> (8 * i)) & 0xff);
            }
            //
            ////////////////////////////////////////////////////
            //Return result.
            return hashCode;
        }

        public static long LongHash(string value)
        {
            //Hash code.
            long hashCode = 0;
            //Do while.
            for (int i = 0; i < value.Length; i++)
            {
                //Get hash code.
                hashCode = (hashCode * 16777619) ^ value[i];
            }
            ////////////////////////////////////////////////////
            //
            //Add the effect of length to the hash code.
            //
            //Get length.
            int length = value.Length;
            //Do while.
            for (int i = 0; i < SizeOf.INTEGER; i++)
            {
                //Add length as a byte.
                hashCode = (hashCode * 16777619) ^ ((length >> (8 * i)) & 0xff);
            }
            //
            ////////////////////////////////////////////////////
            //Return result.
            return hashCode;
        }
    }
}
