using System.Text;

using SimpleTeam.Constant;

namespace SimpleTeam.IO
{
    public abstract class SimpleBuffer
    {
        //Normal Size
        public const int NORMAL_SIZE = 2 * 1024;

        public SimpleBuffer()
        {

        }

        public abstract void Clear();

        public abstract byte[] GetBytes();

        public abstract int GetSize();

        public abstract int GetPosition();

        public abstract void SetPosition(int offset);

        public abstract byte GetByte();

        public abstract int PutByte(byte value);

        public abstract short GetShort();

        public abstract int PutShort(short value);

        public abstract int GetInteger();

        public abstract int PutInteger(int value);

        public abstract long GetLong();

        public abstract int PutLong(long value);

        public abstract void GetBytes(byte[] bytes);

        public abstract void GetBytes(byte[] bytes, int offset, int length);

        public abstract int PutBytes(byte[]? bytes);

        public abstract int PutBytes(byte[]? bytes, int offset, int length);

        public string? GetString()
        {
            //Get length.
            int length = GetInteger();
            //Check result.
            if (length <= 0) return null;
            //Create bytes.
            byte[] bytes = new byte[length];
            //Get bytes.
            GetBytes(bytes);
            //Return result.
            return Encoding.Default.GetString(bytes);
        }

        public int PutString(string? value)
        {
            //Check value.
            if (string.IsNullOrEmpty(value))
            {
                //Put value.
                PutInteger(0);
                //Return size.
                return SizeOf.INTEGER;
            }
            //Get bytes.
            byte[] bytes =
                Encoding.Default.GetBytes(value);
            //Put value.
            PutInteger(bytes.Length); PutBytes(bytes);
            //Return size.
            return SizeOf.INTEGER + bytes.Length;
        }

        public static SimpleBuffer CreateBuffer(int sizeType)
        {
            /*
            //Check type.
            switch (sizeType)
            {
                case SizeType.QQKB:
                case SizeType.HQKB:
                case SizeType.QKB:
                case SizeType.HKB:
                case SizeType._1KB:
                case SizeType._2KB:
                case SizeType._4KB:
                case SizeType._8KB:
                case SizeType._16KB:
                case SizeType._32KB:
                case SizeType._64KB:
                case SizeType._128KB:
                case SizeType._256KB:
                case SizeType._512KB:
                case SizeType._1MB:
                case SizeType._2MB:
                case SizeType._4MB:
                case SizeType._8MB:
                case SizeType._16MB:
                case SizeType._32MB:
                case SizeType._64MB:
                    //Return result.
                    return new ByteBuffer(SizeType.GetRealSize(sizeType));
                default:
                    //Throw exception.
                    throw new IOException("invalid type(" + sizeType + ") of size");
            }
            */
#if DEBUG
            //Check size type.
            if (sizeType < SizeType.QQKB || sizeType > SizeType._64MB)
            {
                //Throw exception.
                throw new IOException("invalid type(" + sizeType + ") of size");
            }
#endif
            //Return result.
            return new ByteBuffer(SizeType.GetRealSize(sizeType));
        }
    }
}
