using SimpleTeam.Constant;
using SimpleTeam.Container.File;
using SimpleTeam.Container;
using SimpleTeam.Function;
using SimpleTeam.IO;
using SimpleTeam.Time;

namespace SimpleTeam.Case
{
    internal class QueueFileOperatorCase
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
                    new("container.data");
                //Create file conterin.
                FileContainer container =
                    new MappedFileContainer(file);
                //Create data operator.
                DataFileOperator dataOperator = new (container);
                //Create queue operator.
                QueueFileOperator queueOperator = new (container, IContainer.WITHOUT_LIMIT);

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
                        Console.WriteLine("QueueFileOperatorCase.DoCaseA : empty bytes in array !"); return;
                    }
                    //Keep data.
                    long offset = dataOperator.KeepData(bytes);
                    //Create element.
                    data[i] = new KeyElement(offset.ToString(), bytes);
                }

                //Write.
                Console.WriteLine("Add data to queue !");
                //Get current clock.
                long clock = DateTime.Now.Ticks;
                //Do while.
                for (int i = 0;i < CAPACITY;i ++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Keep data.
                    queueOperator.KeepData(long.Parse(data[i].GetKey()));
                }
                //Calculate consumed time.
                Console.WriteLine("QueueFileOperatorCase.DoCaseA : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Write.
                Console.WriteLine("Remove all data from queue !");
                //Get current clock.
                clock = DateTime.Now.Ticks;
                //Do while.
                for (int i = 0; i < CAPACITY; i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Keep data.
                    long offset = queueOperator.LoadData();
                    //Check result.
                    if (long.Parse(data[i].GetKey()) != offset)
                    {
                        //Write.
                        Console.WriteLine("invalid offset({0}) !", offset);
                    }
                }
                //Calculate consumed time.
                Console.WriteLine("QueueFileOperatorCase.DoCaseA : consumed = " + (DateTime.Now.Ticks - clock) / Millisecond.TICKS + " ms");

                //Keep queue page.
                queueOperator.KeepQueuePage();
                //Try to close.
                container.Close();
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("QueueFileOperatorCase.DoCaseA : " + e.Message);
                Console.WriteLine("QueueFileOperatorCase.DoCaseA : unexpected exit !");
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
                //Create data operator.
                DataFileOperator dataOperator = new(container);
                //Create queue operator.
                QueueFileOperator queueOperator = new(container, IContainer.WITHOUT_LIMIT);

                //Write.
                Console.WriteLine("Add all data into queue !");
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
                        Console.WriteLine("QueueFileOperatorCase.DoCaseB : empty bytes in array !"); return;
                    }
                    //Keep data.
                    long offset = dataOperator.KeepData(bytes);
                    //Create element.
                    data[i] = new KeyElement(offset.ToString(), bytes);
                    //Keep it in queue.
                    queueOperator.KeepData(offset);
                }

                //Write.
                Console.WriteLine("Remove all data from queue !");
                //Do while.
                for (int i = 0;i < CAPACITY;i++)
                {
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Load data from queue.
                    long offset = queueOperator.LoadData();
                    //Check result.
                    if(offset == -1 || offset != long.Parse(data[i].GetKey()))
                    {
                        Console.WriteLine("invalid data offset !"); return;
                    }
                }

                //Write.
                Console.WriteLine("Clear all data from queue !");
                //Remove all data.
                queueOperator.ClearAll();
                //Keep queue page.
                queueOperator.KeepQueuePage();
                //Try to close.
                container.Close();
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("QueueFileOperatorCase.DoCaseB : " + e.Message);
                Console.WriteLine("QueueFileOperatorCase.DoCaseB : unexpected exit !");
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
                Console.WriteLine("QueueFileOperatorCase.main : " + e.Message);
                Console.WriteLine("QueueFileOperatorCase.main : unexpected exit !");
            }
        }
    }
}
