using System.Collections;

using SimpleTeam.Function;
using SimpleTeam.Container;
using SimpleTeam.Container.Hash;

namespace SimpleTeam.Case
{
    internal class SimpleHashCase
    {
        //Count.
        private const int COUNT = 20000000;
        //Capacity.
        private const int CAPACITY = 1000000;

        public static void DoCaseC(string[] args)
        {
            try
            {
                //Prepare data.
                KeyElement[] data = new KeyElement[CAPACITY];
                //Do while.
                for(int i = 0; i < CAPACITY; i ++)
                {
                    //Get a random length.
                    int length = SimpleRandom.NextShort();
                    //Check length.
                    if (length < 0) length = -length; length &= 0x0f;

                    //Get random value.
                    byte[] bytes = SimpleRandom.NextBytes(length + 1);
                    //Get key.
                    string key = SimpleHash.LongHash(bytes).ToString();

                    //Create element.
                    data[i] = new KeyElement(key, bytes);
                }

                //Insert count.
                int insert = 0;
                //Delete count.
                int delete = 0;

                //Create hash map.
                Hashtable hashtable = [];
                //Do while.
                for (int i = 0; i < COUNT; i++)
                {
                    //Get index.
                    int index = SimpleRandom.NextInteger() % CAPACITY;
                    //Get element.
                    KeyElement element = data[index];
                    //Get key.
                    string key = element.GetKey();
                    //Get value.
                    byte[]? bytes = (byte[]?)element.GetValue();
                    //Check existance.
                    if (!hashtable.ContainsKey(key))
                    {
                        //Add count.
                        insert ++;
                        //Put value.
                        hashtable.Add(key, bytes);
                    }
                    else
                    {
                        //Check bytes.
                        if (!SimpleTeam.Function.Comparer.
                            Equal((byte[]?)hashtable[key], bytes, true))
                        {
                            //Print.
                            Console.WriteLine("SimpleHashCase.DoCaseC : invalid key-value !");
                        }
                        //Remove it.
                        hashtable.Remove(key); delete ++;
                    }
                    //Print information.
                    if ((i + 1) % 1000000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }
                }
                //Print.
                Console.WriteLine("SimpleHashCase.DoCaseC : insert(" + insert + ")");
                Console.WriteLine("SimpleHashCase.DoCaseC : delete(" + delete + ")");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //Print.
                Console.WriteLine("SimpleHashCase.DoCaseC : " + e.Message);
                Console.WriteLine("SimpleHashCase.DoCaseC : unexpected exit !");
            }
        }

        public static void DoCaseB(string[] args)
        {
            try
            {
                //Prepare data.
                KeyElement[] data = new KeyElement[CAPACITY];
                //Do while.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Get a random length.
                    int length = SimpleRandom.NextShort();
                    //Check length.
                    if (length < 0) length = -length; length &= 0x0f;

                    //Get random value.
                    byte[] bytes = SimpleRandom.NextBytes(length + 1);
                    //Get key.
                    string key = SimpleHash.LongHash(bytes).ToString();

                    //Create element.
                    data[i] = new KeyElement(key, bytes);
                }

                //Insert count.
                int insert = 0;
                //Delete count.
                int delete = 0;

                //Create hash container.
                SimpleHashContainer container = new SimpleHashContainer();
                //Do while.
                for (int i = 0; i < COUNT; i++)
                {
                    //Get index.
                    int index = SimpleRandom.NextInteger() % CAPACITY;
                    //Get element.
                    KeyElement element = data[index];
                    //Get key.
                    string key = element.GetKey();
                    //Get value.
                    byte[]? bytes = (byte[]?)element.GetValue();
                    //Check existance.
                    if (!container.Contains(key))
                    {
                        //Add count.
                        insert ++;
                        //Put value.
                        container.Put(key, bytes);
                    }
                    else
                    {
                        //Check bytes.
                        if (!SimpleTeam.Function.Comparer.
                            Equal((byte[]?)container.Get(key), bytes, true))
                        {
                            //Print.
                            Console.WriteLine("SimpleHashCase.DoCaseB : invalid key-value !");
                        }
                        //Remove it.
                        container.Remove(key); delete ++;
                    }
                    //Print information.
                    if ((i + 1) % 1000000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                        Console.WriteLine("\tmax level = {0}", container.GetMaxLevel());
                    }
                }
                //Print.
                Console.WriteLine("SimpleHashCase.DoCaseB : insert(" + insert + ")");
                Console.WriteLine("SimpleHashCase.DoCaseB : delete(" + delete + ")");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //Print.
                Console.WriteLine("SimpleHashCase.DoCaseB : " + e.Message);
                Console.WriteLine("SimpleHashCase.DoCaseB : unexpected exit !");
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                //Do case.
                //doCaseA(args);
                //Do case.
                DoCaseB(args);
                //Do case.
                //DoCaseC(args);
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("SimpleHashCase.main : " + e.Message);
                Console.WriteLine("SimpleHashCase.main : unexpected exit !");
            }
        }
    }
}
