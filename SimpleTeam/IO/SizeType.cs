namespace SimpleTeam.IO
{
    public class SizeType
    {
        //Killo Bytes
        // 1 KB = 1024 Bytes
        private const int KB = 1024;
        private const int MB = 1024 * KB;

        //Total Types
        public const int TOTAL_TYPES = 21;

        //Quater of QKB
        // 1 QQKB = 64 Bytes
        public const int QQKB = 1;
        //Half of QKB
        // 1 HQKB = 128 Bytes
        public const int HQKB = 2;
        //Quater of KB
        // 1 QKB = 256 Bytes
        public const int QKB = 3;
        //Half of KB
        // 1 HKB = 512 Bytes
        public const int HKB = 4;

        //KBs
        public const int _1KB = 5;
        public const int _2KB = 6;
        public const int _4KB = 7;
        public const int _8KB = 8;
        public const int _16KB = 9;
        public const int _32KB = 10;
        public const int _64KB = 11;
        public const int _128KB = 12;
        public const int _256KB = 13;
        public const int _512KB = 14;
        public const int _1MB = 15;
        public const int _2MB = 16;
        public const int _4MB = 17;
        public const int _8MB = 18;
        public const int _16MB = 19;
        public const int _32MB = 20;
        public const int _64MB = 21;

        public static bool IsValid(int value)
        {
            //Return result.
            return value >= 1 && value <= 21;
        }

        public static int GetSizeType(int size)
        {
            //Check result.
            if (size <= KB / 4 / 4) return QQKB;
            else if (size <= KB / 4 / 2) return HQKB;
            else if (size <= KB / 4) return QKB;
            else if (size <= KB / 2) return HKB;
            else if (size <= 1 * KB) return _1KB;
            else if (size <= 2 * KB) return _2KB;
            else if (size <= 4 * KB) return _4KB;
            else if (size <= 8 * KB) return _8KB;
            else if (size <= 16 * KB) return _16KB;
            else if (size <= 32 * KB) return _32KB;
            else if (size <= 64 * KB) return _64KB;
            else if (size <= 128 * KB) return _128KB;
            else if (size <= 256 * KB) return _256KB;
            else if (size <= 512 * KB) return _512KB;
            else if (size <= 1 * MB) return _1MB;
            else if (size <= 2 * MB) return _2MB;
            else if (size <= 4 * MB) return _4MB;
            else if (size <= 8 * MB) return _8MB;
            else if (size <= 16 * MB) return _16MB;
            else if (size <= 32 * MB) return _32MB;
            else if (size <= 64 * MB) return _64MB;
            else return -1;
        }

        public static int GetRealSize(int value)
        {
            //Check value.
            if (value <= 0)
            {
                throw new IOException("underflowed size type(" + value + ")");
            }
            else if (value <= 4)
            {
                //Return size.
                return 64 << (value - 1);
            }
            else if (value <= 14)
            {
                //Return size.
                return 1024 << (value - 5);
            }
            else if (value <= 21)
            {
                //Return size.
                return 1048576 << (value - 15);
            }
            else
            {
                throw new IOException("overflowed size type(" + value + ")");
            }
        }

        public static string ToString(int value)
        {
            //Check value.
            switch (value)
            {
                case QQKB:
                    return "QQKB";
                case HQKB:
                    return "HQKB";
                case QKB:
                    return "QKB";
                case HKB:
                    return "HKB";
                case _1KB:
                    return "1KB";
                case _2KB:
                    return "2KB";
                case _4KB:
                    return "4KB";
                case _8KB:
                    return "8KB";
                case _16KB:
                    return "16KB";
                case _32KB:
                    return "32KB";
                case _64KB:
                    return "64KB";
                case _128KB:
                    return "128KB";
                case _256KB:
                    return "256KB";
                case _512KB:
                    return "512KB";
                case _1MB:
                    return "1MB";
                case _2MB:
                    return "2MB";
                case _4MB:
                    return "4MB";
                case _8MB:
                    return "8MB";
                case _16MB:
                    return "16MB";
                case _32MB:
                    return "32MB";
                case _64MB:
                    return "64MB";
                default:
                    return "reserved";
            }
        }
    }
}
