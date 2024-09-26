using System.Text;

using SimpleTeam.Constant;
using SimpleTeam.Constant.System;

namespace SimpleTeam.Log
{
    public class RecycleString : IRecycle
    {
        //String builder
        private StringBuilder buffer;

        public RecycleString()
        {
            //Create buffer.
            buffer = new StringBuilder();
        }

        public RecycleString(string value)
        {
            //Create buffer.
            buffer = new StringBuilder(value);
        }

        protected StringBuilder GetBuffer() { return buffer; }

        protected char LastChar()
        {
            //Return result.
            return buffer.Length > 0 ? buffer[^1] : '\0';
        }

        public void Clear()
        {
            //Clear buffer.
            buffer.Clear();
        }

        public int Length()
        {
            //Return result.
            return buffer.Length;
        }

        public new string ToString() { return buffer.ToString(); }

        public virtual void End()
        {
            //Check last char.
            if (LastChar() != LF.VALUE)
            {
                //Append.
                buffer.Append(LineSeprator.VALUE);
            }
        }

        public RecycleString Tab()
        {
            //Append.
            buffer.Append((char)HT.VALUE); return this;
        }

        public RecycleString Skip()
        {
            //Append.
            buffer.Append((char)BS.VALUE); return this;
        }

        public RecycleString NewLine()
        {
            //Append.
            buffer.Append(LineSeprator.VALUE); return this;
        }

        public RecycleString NewLine(string value)
        {
            //Append.
            buffer.Append(value).Append(LineSeprator.VALUE); return this;
        }

        public RecycleString Append(bool value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(byte value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(short value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(int value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(long value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(float value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(double value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(char value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Append(string value)
        {
            //Append.
            buffer.Append(value); return this;
        }

        public RecycleString Insert(bool value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(byte value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(short value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(int value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(long value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(float value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(double value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(char value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString Insert(string value)
        {
            //Insert.
            buffer.Insert(0, value); return this;
        }

        public RecycleString AppendHex(byte value)
        {
            //Return result.
            return AppendHex(true, value);
        }

        public RecycleString AppendHex(bool prefix, byte value)
        {
            //Append.
            if (prefix) buffer.Append("0x");
            //Append.
            buffer.Append(Hex.VALUES[(value >> 4) & 0x0f]).
                Append(Hex.VALUES[value & 0x0f]);
            //Return this.
            return this;
        }

        public RecycleString AppendHex(short value)
        {
            //Return result.
            return AppendHex(true, value);
        }

        public RecycleString AppendHex(bool prefix, short value)
        {
            //Append.
            if (prefix) buffer.Append("0x");
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.SHORT; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(value >> (4 * (2 * SizeOf.SHORT - i - 1))) & 0x0f]);
            }
            //Return this.
            return this;
        }

        public RecycleString AppendHex(int value)
        {
            //Return result.
            return AppendHex(true, value);
        }

        public RecycleString AppendHex(bool prefix, int value)
        {
            //Append.
            if (prefix) buffer.Append("0x");
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.INTEGER; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(value >> (4 * (2 * SizeOf.INTEGER - i - 1))) & 0x0f]);
            }
            //Return this.
            return this;
        }

        public RecycleString AppendHex(long value)
        {
            //Return result.
            return AppendHex(true, value);
        }

        public RecycleString AppendHex(bool prefix, long value)
        {
            //Append.
            if (prefix) buffer.Append("0x");
            //Convert to hex string.
            for (int i = 0; i < 2 * SizeOf.LONG; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(value >> (4 * (2 * SizeOf.LONG - i - 1))) & 0x0f]);
            }
            //Return this.
            return this;
        }

        public RecycleString AppendHex(byte[] bytes)
        {
            //Return result.
            return AppendHex(true, bytes, 0, bytes != null ? bytes.Length : 0);
        }

        public RecycleString AppendHex(bool prefix, byte[] bytes)
        {
            //Return result.
            return AppendHex(prefix, bytes, 0, bytes != null ? bytes.Length : 0);
        }

        public RecycleString AppendHex(byte[] bytes, int length)
        {
            //Return result.
            return AppendHex(true, bytes, 0, length);
        }

        public RecycleString AppendHex(bool prefix, byte[] bytes, int length)
        {
            //Return result.
            return AppendHex(prefix, bytes, 0, length);
        }

        public RecycleString AppendHex(byte[] bytes, int offset, int length)
        {
            //Return result.
            return AppendHex(true, bytes, offset, length);
        }

        public RecycleString AppendHex(bool prefix, byte[] bytes, int offset, int length)
        {
            //Check result.
            if (bytes.Length < length) length = bytes.Length;

            //Append.
            if (prefix) buffer.Append("0x");
            //Convert to hex string.
            for (int i = 0; i < length; i++)
            {
                //Append char.
                buffer.Append(Hex.VALUES[(bytes[offset + i] >> 4) & 0x0f]);
                //Append char.
                buffer.Append(Hex.VALUES[bytes[offset + i] & 0x0f]);
            }
            //Return result.
            return this;
        }
    }
}

