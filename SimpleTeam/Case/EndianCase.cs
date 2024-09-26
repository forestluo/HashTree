using SimpleTeam.Constant;
using SimpleTeam.Function;
using SimpleTeam.Log;

namespace SimpleTeam.Case
{
    internal class EndianCase
    {
        //Count.
        private const int COUNT = 10000000;

        public static void DoCaseA(string[] args)
        {
            byte byteValue = 0x10;
            short shortValue = 0x1234;
            int intValue = 0x12345678;
            byte[] bytes = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0};
            float floatValue = 3.14159f;
            double doubleValue = 3.141592658979;

            //Log something.
            for (int i = 0; i < COUNT; i++)
            {
                HexFormat.ToString(BigEndian.ToBytes(byteValue));
                HexFormat.ToString(BigEndian.ToBytes(shortValue));
                HexFormat.ToString(BigEndian.ToBytes(intValue));
                HexFormat.ToString(bytes);
                HexFormat.ToString(BigEndian.ToBytes(floatValue));
                HexFormat.ToString(BigEndian.ToBytes(doubleValue));

                //Print information.
                if ((i + 1) % 1000000 == 0)
                {
                    Console.WriteLine("{0} converts were done !", i + 1);
                }
            }

            //Big endian.
            Console.WriteLine("byteValue(0x{0:X}) = 0x{1}", byteValue, HexFormat.ToString(BigEndian.ToBytes(byteValue)));
            Console.WriteLine("shortValue(0x{0:X}) = 0x{1}", shortValue, HexFormat.ToString(BigEndian.ToBytes(shortValue)));
            Console.WriteLine("intValue(0x{0:X}) = 0x{1}", intValue, HexFormat.ToString(BigEndian.ToBytes(intValue)));
            Console.WriteLine("floatValue({0}) = 0x{1}", floatValue, HexFormat.ToString(BigEndian.ToBytes(floatValue)));
            Console.WriteLine("doubleValue({0}) = 0x{1}", doubleValue, HexFormat.ToString(BigEndian.ToBytes(doubleValue)));

            bytes = BigEndian.ToBytes(floatValue);
            double fValue = BigEndian.GetFloat(bytes, 0);
            Console.WriteLine("floatValue({0}) => {1}", floatValue, fValue);

            bytes = BigEndian.ToBytes(doubleValue);
            double dValue = BigEndian.GetDouble(bytes, 0);
            Console.WriteLine("doubleValue({0}) => {1}", doubleValue, dValue);
        }

        public static void DoCaseB(string[] args)
        {
            byte byteValue = 0x10;
            short shortValue = 0x1234;
            int intValue = 0x12345678;
            byte[] bytes = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0 };
            float floatValue = 3.14159f;
            double doubleValue = 3.141592658979;

            //Log something.
            for (int i = 0; i < COUNT; i++)
            {
                HexFormat.ToString(LittleEndian.ToBytes(byteValue));
                HexFormat.ToString(LittleEndian.ToBytes(shortValue));
                HexFormat.ToString(LittleEndian.ToBytes(intValue));
                HexFormat.ToString(bytes);
                HexFormat.ToString(LittleEndian.ToBytes(floatValue));
                HexFormat.ToString(LittleEndian.ToBytes(doubleValue));

                //Print information.
                if ((i + 1) % 1000000 == 0)
                {
                    Console.WriteLine("{0} converts were done !", i + 1);
                }
            }

            //Big endian.
            Console.WriteLine("byteValue(0x{0:X}) = 0x{1}", byteValue, HexFormat.ToString(LittleEndian.ToBytes(byteValue)));
            Console.WriteLine("shortValue(0x{0:X}) = 0x{1}", shortValue, HexFormat.ToString(LittleEndian.ToBytes(shortValue)));
            Console.WriteLine("intValue(0x{0:X}) = 0x{1}", intValue, HexFormat.ToString(LittleEndian.ToBytes(intValue)));
            Console.WriteLine("floatValue({0}) = 0x{1}", floatValue, HexFormat.ToString(LittleEndian.ToBytes(floatValue)));
            Console.WriteLine("doubleValue({0}) = 0x{1}", doubleValue, HexFormat.ToString(LittleEndian.ToBytes(doubleValue)));

            bytes = LittleEndian.ToBytes(floatValue);
            double fValue = LittleEndian.GetFloat(bytes, 0);
            Console.WriteLine("floatValue({0}) => {1}", floatValue, fValue);

            bytes = LittleEndian.ToBytes(doubleValue);
            double dValue = LittleEndian.GetDouble(bytes, 0);
            Console.WriteLine("doubleValue({0}) => {1}", doubleValue, dValue);
        }

        public static void Main(string[] args)
        {
            try
            {
                //Do case.
                DoCaseA(args);
                DoCaseB(args);
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("EndianCase.main : " + e.Message);
                Console.WriteLine("EndianCase.main : unexpected exit !");
            }
        }
    }
}
