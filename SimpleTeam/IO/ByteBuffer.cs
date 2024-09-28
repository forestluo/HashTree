using SimpleTeam.Constant;
using SimpleTeam.Function;

namespace SimpleTeam.IO
{
    public class ByteBuffer
        : SimpleBuffer
    {
        //Offset of buffer.
        private int offset;
        //Bytes of buffer.
        private byte[] bytes;

        public ByteBuffer(int size)
        {
#if DEBUG
            //Check size.
            if (size < 0)
            {
                throw new IOException("invalid size(" + size + ") of buffer");
            }
#endif
            //Create bytes.
            bytes = new byte[size];
        }

        public sealed override void Clear()
        {
            //Clear offset.
            offset = 0;
        }

        public sealed override byte[] GetBytes() { return bytes; }

        public sealed override int GetSize() { return bytes.Length; }

        public sealed override int GetPosition() { return offset; }

        public sealed override void SetPosition(int offset)
        {
#if DEBUG
            //Check offset.
            if (offset < 0 || offset >= bytes.Length)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Set value.
            this.offset = offset;
        }

        public sealed override byte GetByte()
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.BYTE)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Get value.
            byte value = bytes[offset];
            //Add offset.
            offset += SizeOf.BYTE;
            //Return result.
            return value;
        }

        public sealed override int PutByte(byte value)
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.BYTE)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Set value.
            bytes[offset] = value;
            //Add offset and length.
            offset += SizeOf.BYTE;
            //Return size.
            return SizeOf.BYTE;
        }

        public sealed override short GetShort()
        {
#if DEBUG
            if(offset < 0 || offset > bytes.Length - SizeOf.SHORT)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Get value.
            short value = BigEndian.GetShort(bytes, offset);
            //Add offset.
            offset += SizeOf.SHORT;
            //Return value.
            return value;
        }

        public sealed override int PutShort(short value)
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.SHORT)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Set short.
            BigEndian.SetShort(bytes, offset, value);
            //Add offset and length.
            offset += SizeOf.SHORT;
            //Return size.
            return SizeOf.SHORT;
        }

        public sealed override int GetInteger()
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.INTEGER)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Get value.
            int value = BigEndian.GetInteger(bytes, offset);
            //Add offset.
            offset += SizeOf.INTEGER;
            //Return value.
            return value;
        }

        public sealed override int PutInteger(int value)
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.INTEGER)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Set integer.
            BigEndian.SetInteger(bytes, offset, value);
            //Add offset and length.
            offset += SizeOf.INTEGER;
            //Return size.
            return SizeOf.INTEGER;
        }

        public sealed override long GetLong()
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.LONG)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Get value.
            long value = BigEndian.GetLong(bytes, offset);
            //Add offset.
            offset += SizeOf.LONG;
            //Return value.
            return value;
        }

        public sealed override int PutLong(long value)
        {
#if DEBUG
            //Check offset.
            if(offset < 0 || offset > bytes.Length - SizeOf.LONG)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Set integer.
            BigEndian.SetLong(bytes, offset, value);
            //Add offset and length.
            offset += SizeOf.LONG;
            //Return result.
            return SizeOf.LONG;
        }

        public sealed override void GetBytes(byte[] bytes)
        {
#if DEBUG
            //Check bytes.
            if (bytes == null || bytes.Length <= 0)
            {
                throw new IOException("null or empty bytes");
            }
            //Check offset.
            if(offset < 0 || offset > this.bytes.Length - bytes.Length)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Copy bytes.
            Array.Copy(this.bytes, offset, bytes, 0, bytes.Length);
            //Add offset.
            offset += bytes.Length;
        }

        public sealed override void GetBytes(byte[] bytes, int offset, int length)
        {
#if DEBUG
            //Check bytes.
            if(bytes == null || bytes.Length <= 0)
            {
                throw new IOException("null or empty bytes");
            }
            //Check offset.
            if(offset < 0 || offset > this.bytes.Length - length)
            {
                throw new IOException("invalid offset(" + offset + ")");
            }
#endif
            //Copy bytes.
            Array.Copy(this.bytes, this.offset, bytes, offset, length);
            //Add offset.
            offset += length;
        }

        public sealed override int PutBytes(byte[]? bytes)
        {
            //Check result.
            if (bytes == null || bytes.Length <= 0) return 0;
            //Copy bytes.
            Array.Copy(bytes, 0, this.bytes, offset, bytes.Length);
            //Add offset and length.
            this.offset += bytes.Length;
            //Return size.
            return bytes.Length;
        }

        public sealed override int PutBytes(byte[]? bytes, int offset, int length)
        {
            //Check result.
            if (bytes == null || bytes.Length <= 0) return 0;
            //Copy bytes.
            Array.Copy(bytes, offset, this.bytes, this.offset, length);
            //Add offset and length.
            this.offset += length;
            //Return size.
            return length;
        }
    }
}
