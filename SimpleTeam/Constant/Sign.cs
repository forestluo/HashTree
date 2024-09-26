namespace SimpleTeam.Constant
{
    public class Sign
    {
        //Negative
        public const int NEGATIVE = -1;
        //Zero
        public const int ZERO = 0;
        //Positive
        public const int POSITIVE = 1;

        public static int GetSign(byte value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }

        public static int GetSign(short value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }

        public static int GetSign(int value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }

        public static int GetSign(long value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }

        public static int GetSign(float value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }

        public static int GetSign(double value)
        {
            //Check value.
            if (value < 0)
            {
                //Return -1.
                return NEGATIVE;
            }
            //Check value.
            if (value > 0)
            {
                //Return 1.
                return POSITIVE;
            }
            //Return 0.
            return ZERO;
        }
    }
}
