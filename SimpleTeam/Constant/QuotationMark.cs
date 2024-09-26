//MMS-WAP
namespace SimpleTeam.Constant
{
    public class QuotationMark
    {
        //Value
        public const int VALUE = '\"';

        /**
         * Whether value is a space.
         *
         * @param value Integer value.
         * @return
         *     <p>Return true, if it is a quotation mark.</p>
         */
        public static bool IsQMChar(int value)
        {
            //Check value.
            return value == VALUE;
        }
    }
}
