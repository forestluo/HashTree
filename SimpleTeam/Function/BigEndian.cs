using SimpleTeam.Constant;
using System.Xml.Schema;

namespace SimpleTeam.Function
{
    public class BigEndian
    {
        public static byte[] ToBytes(byte value)
        {
            //Create bytes.
            byte[] bytes = [SizeOf.BYTE];
            //Set value.
            bytes[0] = value;
            //Return bytes.
            return bytes;
        }

        public static byte[] ToBytes(short value)
        {
            //Create bytes.
            byte[] bytes = [SizeOf.SHORT];
            //Set value.
            for (int i = 0; i < SizeOf.SHORT; i++)
            {
                //Network order.
                bytes[SizeOf.SHORT - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
            //Return bytes.
            return bytes;
        }

        public static byte[] ToBytes(int value)
        {
            //Create bytes.
            byte[] bytes = [SizeOf.INTEGER];
            //Set value.
            for (int i = 0; i < SizeOf.INTEGER; i++)
            {
                //Network order.
                bytes[SizeOf.INTEGER - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
            //Return bytes.
            return bytes;
        }

        public static byte[] ToBytes(long value)
        {
            //Create bytes.
            byte[] bytes = [SizeOf.LONG];
            //Set value.
            for (int i = 0; i < SizeOf.LONG; i++)
            {
                //Network order.
                bytes[SizeOf.LONG - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
            //Return bytes.
            return bytes;
        }

        public static byte[] ToBytes(float value)
        {
            //Get bytes (in little endian)
            byte[] bytes =
                BitConverter.GetBytes(value);
            //Reverse it.
            Array.Reverse(bytes); return bytes;
        }

        public static byte[] ToBytes(double value)
        {
            //Get bytes (in little endian)
            byte[] bytes =
                BitConverter.GetBytes(value);
            //Reverse it.
            Array.Reverse(bytes); return bytes;
        }

        public static short GetShort(byte[] bytes, int offset)
        {
            //Add value.
            short value = 0;
            //Get integer value.
            for (int i = 0; i < SizeOf.SHORT; i++)
            {
                //Shift left.
                value <<= 8;
                //Add value.
                value |= (short)(bytes[offset + i] & 0xff);
            }
            //Return value.
            return value;
        }

        public static void SetShort(byte[] bytes, int offset, short value)
        {
            //Set integer value.
            for (int i = 0; i < SizeOf.SHORT; i++)
            {
                //Set value.
                bytes[offset + SizeOf.SHORT - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
        }

        public static int GetInteger(byte[] bytes, int offset)
        {
            //Add value.
            int value = 0;
            //Get integer value.
            for (int i = 0; i < SizeOf.INTEGER; i++)
            {
                //Shift left.
                value <<= 8;
                //Add value.
                value |= bytes[offset + i] & 0xff;
            }
            //Return value.
            return value;
        }

        public static void SetInteger(byte[] bytes, int offset, int value)
        {
            //Set integer value.
            for (int i = 0; i < SizeOf.INTEGER; i++)
            {
                //Set value.
                bytes[offset + SizeOf.INTEGER - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
        }

        public static long GetLong(byte[] bytes, int offset)
        {
            //Add value.
            long value = 0;
            //Get integer value.
            for (int i = 0; i < SizeOf.LONG; i++)
            {
                //Shift left.
                value <<= 8;
                //Add value.
                value |= (long)(bytes[offset + i] & 0xff);
            }
            //Return value.
            return value;
        }

        public static void SetLong(byte[] bytes, int offset, long value)
        {
            //Set integer value.
            for (int i = 0; i < SizeOf.LONG; i++)
            {
                //Set value.
                bytes[offset + SizeOf.LONG - i - 1] = (byte)((value >> (i * 8)) & 0xff);
            }
        }

        public static float GetFloat(byte[] bytes, int offset)
        {
            //Copy bytes.
            byte[] newBytes = [SizeOf.FLOAT];
            //Little endian.
            for (int i = 0; i < SizeOf.FLOAT; i ++)
            {
                //Set value.
                newBytes[i] = bytes[offset + SizeOf .FLOAT - i - 1];
            }
            //Return result.
            return BitConverter.ToSingle(newBytes);
        }

        public static void SetFloat(byte[] bytes, int offset, float value)
        {
            //Get bytes.
            byte[] newBytes = BitConverter.GetBytes(value);
            //Little endian.
            for (int i = 0; i < SizeOf.FLOAT; i++)
            {
                //Set value.
                bytes[offset + i] = newBytes[SizeOf.FLOAT - i - 1];
            }
        }

        public static double GetDouble(byte[] bytes, int offset)
        {
            //Copy bytes.
            byte[] newBytes = [SizeOf.DOUBLE];
            //Little endian.
            for (int i = 0; i < SizeOf.DOUBLE; i++)
            {
                //Set value.
                newBytes[i] = bytes[offset + SizeOf.DOUBLE - i - 1];
            }
            //Return result.
            return BitConverter.ToDouble(newBytes);
        }

        public static void SetDouble(byte[] bytes, int offset, double value)
        {
            //Get bytes.
            byte[] newBytes = BitConverter.GetBytes(value);
            //Little endian.
            for (int i = 0; i < SizeOf.DOUBLE; i++)
            {
                //Set value.
                bytes[offset + i] = newBytes[SizeOf.DOUBLE - i - 1];
            }
        }
    }
}
