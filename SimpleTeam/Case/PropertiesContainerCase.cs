using SimpleTeam.Container.Properties;

namespace SimpleTeam.Case
{
    internal class PropertiesContainerCase
    {
        public static void DoCaseA(string[] args)
        {
            byte byteValue = 0x10;
            short shortValue = 0x1234;
            int intValue = 0x12345678;
            byte[] bytes = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0 };
            float floatValue = 3.14159f;
            double doubleValue = 3.141592658979;

            HashtablePropertiesContainer container = new ();

            //Put values.
            container.SetByte("byteValue", byteValue);
            container.SetShort("shortValue", shortValue);
            container.SetInteger("intValue", intValue);
            container.SetBytes("bytes", bytes);
            container.SetFloat("floatValue", floatValue);
            container.SetDouble("doubleValue", doubleValue);
            //Dump.
            container.Dump();
        }

        public static void Main(string[] args)
        {
            try
            {
                //Do case.
                DoCaseA(args);
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("PropertiesContainerCase.main : " + e.Message);
                Console.WriteLine("PropertiesContainerCase.main : unexpected exit !");
            }
        }
    }
}
