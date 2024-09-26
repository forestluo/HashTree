
using SimpleTeam.Constant;
using SimpleTeam.Time;

namespace SimpleTeam.Function
{
    public class SimpleRandom
    {
        //Random
        private static readonly Random random;

        static SimpleRandom()
        {
            //Set random.
            random = new Random((int)Second.CurrentTime());
        }

        public static byte NextByte()
        {
            //Return value.
            return (byte)random.Next();
        }

        public static short NextShort()
        {
            //Return value.
            return (short)random.Next();
        }

        public static int NextInteger()
        {
            //Return value.
            return random.Next();
        }

        public static long NextLong()
        {
            //Return value.
            return random.NextInt64();
        }

        public static float NextFloat()
        {
            //Return value.
            return random.NextSingle();
        }

        public static double NextDouble()
        {
            //Return value.
            return random.NextDouble();
        }

        public static byte[] NextBytes(int length)
        {
#if DEBUG
            if (length <= 0)
            {
                throw new ArgumentException("invalid length(" + length + ")");
            }
#endif
            //Create buffer.
            byte[] bytes = new byte[length];
            //Get random bytes.
            random.NextBytes(bytes);
            //Return value.
            return bytes;
        }

        public static string NextDecimal(int length)
        {
#if DEBUG
            if (length <= 0)
            {
                throw new ArgumentException("invalid length(" + length + ")");
            }
#endif
            //Create a char buffer to put random letters and numbers in.
            char[] chars = new char[length];
            //Get random buffer.
            for (int i = 0; i < length; i++)
            {
                //Get random.
                int value = random.Next() % 10;
                //Check random.
                //Get decimal.
                chars[i] = SimpleTeam.Constant.Decimal.VALUES[value];
            }
            //Return string.
            return new string(chars);
        }

        public static string NextString(int length)
        {
            //Create a char buffer to put random letters and numbers in.
            char[] chars = new char[length];
            //Get random buffer.
            for (int i = 0; i < length; i++)
            {
                //Get random.
                int value = random.Next() & 0x03;
                //Check result.
                switch (value)
                {
                    case 0:
                        //Get random.
                        value = random.Next() % 26;
                        //Get lower alpha.
                        chars[i] = LowerAlpha.VALUES[value];
                        break;
                    case 1:
                        //Get random.
                        value = random.Next() % 26;
                        //Get upper alpha.
                        chars[i] = UpperAlpha.VALUES[value];
                        break;
                    case 2:
                        //Get random.
                        value = random.Next() % 10;
                        //Get digit.
                        chars[i] = SimpleTeam.Constant.Decimal.VALUES[value];
                        break;
                }
            }
            //Return string.
            return new string(chars);
        }
    }
}
