using SimpleTeam.IO;
using SimpleTeam.Time;
using SimpleTeam.Constant;
using SimpleTeam.Function;
using SimpleTeam.Container;
using SimpleTeam.Container.File;

namespace SimpleTeam.Case
{
    internal class DataFileOperatorCase
    {
        //Count
        private const int COUNT = 1000000;
        //Capacity.
        private const int CAPACITY = 100000;

        public static void DoCaseA(string[] args)
        {
            try
            {
                //Create file.
                FileInfo file =
                    new ("container.data");
                //Create file conterin.
                FileContainer container =
                    new MappedFileContainer(file);
                //Create file operator.
                DataFileOperator dataOperator = new (container);

                //Insert count.
                int insert = 0;
                //Delete count.
                int delete = 0;

                //Prepare data.
                KeyElement[] data = new KeyElement[CAPACITY];
                //Do while.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get random length.
                    int length = SimpleRandom.NextInteger() & 0xFFFF;
                    //Check length.
                    if(length > SizeType.GetRealSize(SizeType._64MB))
                    {
                        //Adjust length.
                        length = SizeType.GetRealSize(SizeType._64MB) - PageDescription.SIZE;
                    }
                    else if(length < SizeType.GetRealSize(SizeType.QQKB))
                    {
                        //Adjust length.
                        length = SizeType.GetRealSize(SizeType.QQKB) - PageDescription.SIZE;
                    }
                    //Get random value.
                    byte[] bytes = SimpleRandom.NextBytes(length);
                    //Check result.
                    if(Empty.IsNullOrEmpty(bytes))
                    {
                        Console.WriteLine("DataFileOperatorCase.DoCaseA : empty bytes in array !"); return;
                    }
                    //Keep data.
                    long offset = dataOperator.KeepData(bytes);
                    //Create element.
                    data[i] = new KeyElement(offset.ToString(), bytes); insert ++;
                }

                //Write.
                Console.WriteLine("Read all data !");
                //Get current clock.
                long clock = DateTime.Now.Ticks;
                //Read all data.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get key.
                    string key = data[i].GetKey();
                    //Get long.
                    long keyValue = long.Parse(key);
                    //Get bytes.
                    byte[] bytes = dataOperator.LoadData(keyValue);
                    //Get value.
                    object? value = data[i].GetValue();
                    //Check bytes.
                    if (!Function.Comparer.Equal((byte[] ?)value, bytes, true))
                    {
                        Console.WriteLine("data(key = {0}) is invalid !", keyValue);
                    }
                }
                //Calculate consumed time.
                Console.WriteLine("DataFileOperatorCase.DoCaseA : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Write.
                Console.WriteLine("Remove all data !");
                //Get current clock.
                clock = DateTime.Now.Ticks;
                //Remove all data.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get key.
                    string key = data[i].GetKey();
                    //Get long.
                    long keyValue = long.Parse(key);
                    //Get bytes.
                    byte[] bytes = dataOperator.RemoveData(keyValue); delete ++;
                    //Get value.
                    object? value = data[i].GetValue();
                    //Check bytes.
                    if (!Comparer.Equal((byte[]?)value, bytes, true))
                    {
                        Console.WriteLine("data(key = {0}) is invalid !", keyValue);
                    }
                }
                //Calculate consumed time.
                Console.WriteLine("DataFileOperatorCase.DoCaseA : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Write.
                Console.WriteLine("Write all data !");
                //Get current clock.
                clock = DateTime.Now.Ticks;
                //Write all data.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get value.
                    object? value = data[i].GetValue();
                    //Check value.
                    if(value == null)
                    {
                        Console.WriteLine("DataFileOperatorCase.DoCaseA : empty bytes in array !"); return;
                    }
                    //Keep data.
                    long offset = dataOperator.KeepData((byte[])value); insert ++;
                    //Set key.
                    data[i].SetKey(offset.ToString());
                }
                //Calculate consumed time.
                Console.WriteLine("DataFileOperatorCase.DoCaseA : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Try to close.
                container.Close();
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("DataFileOperatorCase.DoCaseA : " + e.Message);
                Console.WriteLine("DataFileOperatorCase.DoCaseA : unexpected exit !");
            }
        }

        public static void DoCaseB(string[] args)
        {
            try
            {
                //Create file.
                FileInfo file =
                    new("container.data");
                //Create file conterin.
                FileContainer container =
                    new MappedFileContainer(file);
                //Create file operator.
                DataFileOperator dataOperator = new(container);

                //Insert count.
                int insert = 0;
                //Delete count.
                int delete = 0;

                //Prepare data.
                KeyElement[] data = new KeyElement[CAPACITY];
                //Do while.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get random length.
                    int length = SimpleRandom.NextInteger() & 0xFFFF;
                    //Check length.
                    if (length > SizeType.GetRealSize(SizeType._64MB))
                    {
                        //Adjust length.
                        length = SizeType.GetRealSize(SizeType._64MB) - PageDescription.SIZE;
                    }
                    else if (length < SizeType.GetRealSize(SizeType.QQKB))
                    {
                        //Adjust length.
                        length = SizeType.GetRealSize(SizeType.QQKB) - PageDescription.SIZE;
                    }
                    //Get random value.
                    byte[] bytes = SimpleRandom.NextBytes(length);
                    //Check result.
                    if (Empty.IsNullOrEmpty(bytes))
                    {
                        Console.WriteLine("DataFileOperatorCase.DoCaseA : empty bytes in array !"); return;
                    }
                    //Keep data.
                    long offset = dataOperator.KeepData(bytes);
                    //Create element.
                    data[i] = new KeyElement(offset.ToString(), bytes); insert ++;
                }

                //Write.
                Console.WriteLine("Randomly add or remove data !");
                //Get current clock.
                long clock = DateTime.Now.Ticks;

                //Do while.
                for (int i = 0; i < COUNT; i ++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Get random value.
                    int index = SimpleRandom.NextInteger() % CAPACITY;
                    //Get key.
                    string key = data[index].GetKey();
                    //Get value.,
                    object? value = data[index].GetValue();
                    //Check result.
                    if (value == null)
                    {
                        Console.WriteLine("DataFileOperatorCase.DoCaseB : null bytes in array !"); return;
                    }
                    //Get bytes.
                    byte[] bytes = (byte[])value;

                    //Check key.
                    if (string.IsNullOrEmpty(key))
                    {
                        //Keep data.
                        long offset =
                            dataOperator.KeepData(bytes);
                        //Set key.
                        data[index].SetKey(offset.ToString()); insert ++;
                    }
                    else
                    {
                        
                        //Get offset.
                        long offset = long.Parse(key);
                        //Clear key.
                        data[index].SetKey(string.Empty);
                        //Remove data.
                        byte[] dataBytes =
                            dataOperator.RemoveData(offset); delete ++;
                        //Check bytes.
                        if (!Comparer.Equal(dataBytes, bytes, true))
                        {
                            Console.WriteLine("data(key = {0}) is invalid !", offset);
                        }
                    }
                }

                //Calculate consumed time.
                Console.WriteLine("DataFileOperatorCase.DoCaseB : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Try to close.
                container.Close();
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("DataFileOperatorCase.DoCaseB : " + e.Message);
                Console.WriteLine("DataFileOperatorCase.DoCaseB : unexpected exit !");
            }
        }

        public static void Main(string[] args)
        {
            try
            {
                //Do case.
                DoCaseB(args);
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("DataFileOperatorCase.main : " + e.Message);
                Console.WriteLine("DataFileOperatorCase.main : unexpected exit !");
            }
        }
    }
}
