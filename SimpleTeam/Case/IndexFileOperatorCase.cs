using System.Collections;
using System.ComponentModel;

using SimpleTeam.Time;
using SimpleTeam.Function;
using SimpleTeam.Container;
using SimpleTeam.Container.File;

namespace SimpleTeam.Case
{
    internal class IndexFileOperatorCase
    {
        public static void DoAction(Hashtable hashtable,
            FileContainer container, IndexFileOperator indexOperator)
        {
            try
            {
                //Count.
                int size = 5000000;
                //Load count.
                int loadCount = 0;
                //Insert count.
                int insertCount = 0;
                //Remote count.
                int removeCount = 0;
                //Replace count.
                int replaceCount = 0;
                //Get current time.
                long startTime = Millisecond.CurrentTime();
                long currentTime = Millisecond.CurrentTime();
                //Print.
                Console.WriteLine("Do action !");
                //Do while.
                for (int i = 0; i < 10 * size; i ++)
                {
                    //Print.
                    //Console.WriteLine("IndexFileOperatorCase.DoAction : one opeartion !");
                    //Print information.
                    if ((i + 1) % 1000000 == 0)
                    {
                        //Get time.
                        long timespan =
                            Millisecond.CurrentTime() - currentTime;
                        //Set current time.
                        currentTime = Millisecond.CurrentTime();

                        Console.WriteLine("{0} counts reached !", i + 1);
                        //Print.
                        Console.WriteLine("IndexFileOperatorCase.DoAction : consumed " + (timespan) + "ms !");
                        //Print.
                        Console.WriteLine("\toperation per time = {0}ms/op", timespan / 1000000.0f);
                        //Print.
                        Console.WriteLine("\tindex size = " + indexOperator.GetSize());
                        Console.WriteLine("\tindex count = " + indexOperator.GetCount());
                        //Check size.
                        if (indexOperator.GetSize() > 0)
                        {
                            Console.WriteLine("\tstoring effecience = " + indexOperator.GetCount() / (float)indexOperator.GetSize());
                        }
                        Console.WriteLine("\tfile size = " + container.GetLength());
                        Console.WriteLine("\tpage size = " + container.GetSize());
                        Console.WriteLine("\tpage count = " + container.GetCount());
                        //Action count.
                        Console.WriteLine("\tload action count = " + loadCount);
                        Console.WriteLine("\tremove action count = " + removeCount);
                        Console.WriteLine("\tinsert action count = " + insertCount);
                        Console.WriteLine("\treplace action count = " + replaceCount);
                        Console.WriteLine("\ttotal action count = " +
                                (insertCount + loadCount + removeCount + replaceCount));
                        //Bytes count.
                        Console.WriteLine("\tcontainer read bytes = " + container.GetReadCount());
                        Console.WriteLine("\tcontainer write bytes = " + container.GetWriteCount());
                        Console.WriteLine("\tcontainer remains = " + hashtable.Count);
                        //Get timespan.
                        timespan = currentTime - startTime;
                        //Print.
                        Console.WriteLine("\tcontainer read speed = " + ((1000 * (container.GetReadCount() >> 20)) / (float)timespan) + "MB/s");
                        Console.WriteLine("\tcontainer write speed = " + ((1000 * (container.GetWriteCount() >> 20)) / (float)timespan) + "MB/s");
                    }

                    //Get key value.
                    long keyValue =
                        SimpleRandom.NextLong() % size;
                    //Check value.
                    if (keyValue < 0) keyValue = -keyValue;
                    //Print.
                    //Console.WriteLine("\tkey value = " + keyValue);

                    //Check value.
                    long dataValue = -1L;
                    //Check result.
                    if (!hashtable.ContainsKey(keyValue))
                    {
                        //Get random value.
                        long randomValue =
                            (SimpleRandom.NextLong() % size) << 6;
                        //Check random value.
                        if (randomValue < 0) randomValue = -randomValue;
                        //Print.
                        //Console.WriteLine("\trandom value = " + randomValue);

                        //Add insert count.
                        insertCount ++;
                        //Put it into hash table.
                        hashtable.Add(keyValue, randomValue);
                        //Keep key value.
                        if (indexOperator.KeepData(keyValue, randomValue) == -1L)
                        {
                            //Print.
                            //Console.WriteLine("\t... data(" + keyValue + ") was kept !");
                        }
                        else
                        {
                            //Try to keep again.
                            long value = indexOperator.KeepData(keyValue, randomValue);
                            //Print.
                            Console.WriteLine("\t... fail to keep data(" + keyValue + ") !");
                            break;
                        }
                    }
                    else
                    {
                        //Get action code.
                        int actionCode
                            = SimpleRandom.NextInteger() % 3;
                        //Check action code.
                        if (actionCode == 0)
                        {
                            //Add remove count.
                            removeCount ++;
                            //Put it into hash table.
                            dataValue = (long)hashtable[keyValue]!;
                            //Remove key.
                            hashtable.Remove(keyValue);
                            //Load key value.
                            if (indexOperator.RemoveData(keyValue) == dataValue)
                            {
                                //Print.
                                //Console.WriteLine("\t... data(" + keyValue + ") was removed !");
                            }
                            else
                            {
                                //Remove data.
                                long value = indexOperator.RemoveData(keyValue);
                                //Print.
                                Console.WriteLine("\t... fail to remove data(" + keyValue + ") !");
                                break;
                            }
                        }
                        else if (actionCode == 1)
                        {
                            //Add load count.
                            loadCount ++;
                            //Put it into hash table.
                            dataValue = (long)hashtable[keyValue]!;
                            //Load key value.
                            if (indexOperator.LoadData(keyValue) == dataValue)
                            {
                                //Print.
                                //Console.WriteLine("\t... data(" + keyValue + ") was loaded !");
                            }
                            else
                            {
                                long value = indexOperator.LoadData(keyValue);
                                //Print.
                                Console.WriteLine("\t... fail to load data(" + keyValue + ") !");
                                break;
                            }
                        }
                        else
                        {
                            //Add replace count.
                            replaceCount ++;
                            //Get random value.
                            long randomValue =
                                (SimpleRandom.NextLong() % size) << 6;
                            //Check random value.
                            if (randomValue < 0) randomValue = -randomValue;

                            //Put it into hash table.
                            dataValue = (long)hashtable[keyValue]!;
                            //Set new value.
                            hashtable[keyValue] = randomValue;
                            //Load key value.
                            if (indexOperator.KeepData(keyValue, randomValue) == dataValue)
                            {
                                //Print.
                                //Console.WriteLine("\t... data(" + keyValue + ") was kept !");
                            }
                            else
                            {
                                //Try to keep again.
                                long value = indexOperator.KeepData(keyValue, randomValue);
                                //Print.
                                Console.WriteLine("\t... fail to keep data(" + keyValue + "," + randomValue + ") !");
                                break;
                            }
                        }
                    }
                }
                /*
                //Get time.
                long timespan = Millisecond.CurrentTime() - currentTime;

                Console.WriteLine("\tindex size = " + indexOperator.GetSize());
                Console.WriteLine("\tindex count = " + indexOperator.GetCount());
                //Check size.
                if(indexOperator.GetSize() > 0)
                {
                    Console.WriteLine("\teffecience = " + indexOperator.GetCount() / (float)indexOperator.GetSize());
                }
                //Print.
                Console.WriteLine("IndexFileOperatorCase.doAction : consumed " + (timespan) +  "ms !");
                Console.WriteLine("\tfile size = " + container.GetLength());
                Console.WriteLine("\tcontainer size = " + container.GetSize());
                Console.WriteLine("\tcontainer count = " + container.GetCount());
                Console.WriteLine("\tload count = " + loadCount);
                Console.WriteLine("\tremove count = " + removeCount);
                Console.WriteLine("\tinsert count = " + insertCount);
                Console.WriteLine("\treplace count = " + replaceCount);
                Console.WriteLine("\ttotal count = " +
                        (insertCount + loadCount + removeCount + replaceCount));
                    Console.WriteLine("\tcontainer read bytes = " + container.GetReadCount());
                Console.WriteLine("\tcontainer write bytes = " + container.GetWriteCount());
                Console.WriteLine("\tcontainer remains = " + hashtable.Count);
                Console.WriteLine("\tcontainer read speed = " + ((1000 * (container.GetReadCount() >> 20)) / (float) timespan) + "MB/s");
                Console.WriteLine("\tcontainer write speed = " + ((1000 * (container.GetWriteCount() >> 20)) / (float) timespan) + "MB/s");
                */
            }
            catch(Exception e)
            {
			    //Print.
			    Console.WriteLine("IndexFileOperatorCase.DoAction : " + e.Message);
                Console.WriteLine("IndexFileOperatorCase.DoAction : unexpected exit !");
            }
        }

        public static void ClearAll(FileInfo file, long offset)
        {
            try
            {
                //File container.
                FileContainer container = new MappedFileContainer(file);
                //Create hash file operator.
                IndexFileOperator indexOperator = new (container, offset);

                //Print.
                Console.WriteLine("IndexFileOperatorCase.ClearAll : reopen again !");
                //Get current time.
                long currentTime = Millisecond.CurrentTime();
                //Clear all.
                indexOperator.ClearAll();
                //Get time.
                long timespan = Millisecond.CurrentTime() - currentTime;
                //Print.
                Console.WriteLine("IndexFileOperatorCase.ClearAll : consumed " + (timespan) + "ms !");
                Console.WriteLine("\tcontainer size = " + container.GetSize());
                Console.WriteLine("\tcontainer count = " + container.GetCount());
                Console.WriteLine("\tcontainer read bytes = " + container.GetReadCount());
                Console.WriteLine("\tcontainer write bytes = " + container.GetWriteCount());
                //Close.
                indexOperator.Close();
                //Close.
                container.Close();
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("IndexFileOperatorCase.ClearAll : " + e.Message);
                Console.WriteLine("IndexFileOperatorCase.ClearAll : unexpected exit !");
            }
        }

        public static void DoCaseA(string[] args)
        {
            try
            {
                //Create file.
                FileInfo file = new FileInfo("container.dat");
                //Check existance.
                if (file.Exists) file.Delete();
                //Offset.
                long offset;
                //Create hashtable.
                Hashtable hashtable = new ();
                //Open.
                {
                    //Write.
                    Console.WriteLine("Open index data file !");
                    //Create file container.
                    FileContainer container = new MappedFileContainer(new FileInfo("container.dat"));
                    //Create hash file operator.
                    IndexFileOperator indexOperator = new (container, Container.IContainer.WITHOUT_LIMIT);
                    //Get position.
                    offset = indexOperator.GetEntrance();
                    //Do action.
                    DoAction(hashtable, container, indexOperator);
                    //Close.
                    indexOperator.Close();
                    //Close.
                    container.Close();
                    //Write.
                    Console.WriteLine("Close index data file !");
                }
                //Open.
                {
                    //Write.
                    Console.WriteLine("Open index data file !");
                    //Create file container.
                    FileContainer container = new MappedFileContainer(new FileInfo("container.dat"));
                    //Create hash file operator.
                    IndexFileOperator indexOperator = new (container, offset);
                    //Do action.
                    DoAction(hashtable, container, indexOperator);
                    //Close.
                    indexOperator.Close();
                    //Close.
                    container.Close();
                    //Write.
                    Console.WriteLine("Close index data file !");
                }

                //Open again.
                {
                    FileContainer container = new MappedFileContainer(new FileInfo("container.dat"));
                    //Create hash file operator.
                    IndexFileOperator indexOperator = new (container, offset);

                    //Print.
                    Console.WriteLine("IndexFileOperatorCase.doCaseA : reopen again !");
                    //Get current time.
                    long currentTime = Millisecond.CurrentTime();
                    //Do while.
                    foreach(long key in hashtable.Keys)
                    {
                        //Get value.
                        long value = (long)hashtable[key]!;

                        //Get it from file.
                        long dataValue = indexOperator.LoadData(key);
                        //Check result.
                        if (dataValue != value)
                        {
                            Console.WriteLine("invalid data value(" + dataValue + ")");
                        }
                    }
                    //Get time.
                    long timespan = Millisecond.CurrentTime() - currentTime;
                    //Print.
                    Console.WriteLine("IndexFileOperatorCase.doCaseA : consumed " + (timespan) + "ms !");

                    //Get current time.
                    currentTime = Millisecond.CurrentTime();
                    //Clear all.
                    indexOperator.ClearAll();
                    //Get time.
                    timespan = Millisecond.CurrentTime() - currentTime;
                    //Print.
                    Console.WriteLine("IndexFileOperatorCase.doCaseA : consumed " + (timespan) + "ms !");
                    Console.WriteLine("\tcontainer size = " + container.GetSize());
                    Console.WriteLine("\tcontainer count = " + container.GetCount());
                    Console.WriteLine("\tcontainer read bytes = " + container.GetReadCount());
                    Console.WriteLine("\tcontainer write bytes = " + container.GetWriteCount());
                    //Close.
                    indexOperator.Close();
                    //Close.
                    container.Close();
                }
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("IndexFileOperatorCase.doCaseA : " + e.Message);
                Console.WriteLine("IndexFileOperatorCase.doCaseA : unexpected exit !");
            }
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
                Console.WriteLine("IndexFileOperatorCase.main : " + e.Message);
                Console.WriteLine("IndexFileOperatorCase.main : unexpected exit !");
            }
        }
    }
}
