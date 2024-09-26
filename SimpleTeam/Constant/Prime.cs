﻿namespace SimpleTeam.Constant
{
    public class Prime
    {
        public static readonly int[] COPRIMES =
        [
            107, 109, 113, 127, 131, 137, 139, 149, 151, 157,
            163, 167, 173, 179, 181, 191, 193, 197, 199, 211,
            217, 223, 227, 229, 233, 239, 241, 247, 251, 253,
            255, 256
        ];

        public static readonly int[] VALUES =
        [
            2, 3, 5, 7, 11, 13, 17, 19, 23,
            29, 31, 37, 41, 43, 47, 53, 59, 61,
            67, 71, 73, 79, 83, 89, 97, 101, 103,
            107, 109, 113, 127, 131, 137, 139, 149, 151,
            157, 163, 167, 173, 179, 181, 191, 193, 197,
            199, 211, 223, 227, 229, 233, 239, 241, 251,
            257, 263, 269, 271, 277, 281, 283, 293, 307,
            311, 313, 317, 331, 337, 347, 349, 353, 359,
            367, 373, 379, 383, 389, 397, 401, 409, 419,
            421, 431, 433, 439, 443, 449, 457, 461, 463,
            467, 479, 487, 491, 499, 503, 509, 521, 523,
            541, 547, 557, 563, 569, 571, 577, 587, 593,
            599, 601, 607, 613, 617, 619, 631, 641, 643,
            647, 653, 659, 661, 673, 677, 683, 691, 701,
            709, 719, 727, 733, 739, 743, 751, 757, 761,
            769, 773, 787, 797, 809, 811, 821, 823, 827,
            829, 839, 853, 857, 859, 863, 877, 881, 883,
            887, 907, 911, 919, 929, 937, 941, 947, 953,
            967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021,
        ];

        public static bool IsPrime(int value)
        {
            //Check prime.
            for (int i = 2; i * i <= value; i++)
            {
                //Check value.
                if ((value % i) == 0) return false;
            }
            //Return true.
            return true;
        }

        public static int GetMaxPrime(int value)
        {
            //Do while.
            for (int i = value; i >= 2; i--)
            {
                //Return result.
                if (IsPrime(i)) return i;
            }
            //Return -1.
            return -1;
        }

        public static int GetRemainder(string? value, int prime)
        {
#if DEBUG
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("value is null or empty");
            }
#endif
            //Remainder
            int remainder = 0;
            //Every char is devided by prime.
            for (int i = 0; i < value.Length; i++)
            {
                //Get remainder.
                Math.DivRem(value[i] + (remainder << 16), prime, out remainder);
            }
            //Return remainder.
            return remainder;
        }
    }
}
