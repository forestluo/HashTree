using SimpleTeam.Time;
using SimpleTeam.Function;
using SimpleTeam.Container.List;

namespace SimpleTeam.Case
{
    internal class SimpleListContainerCase
    {
        //Count
        private const int COUNT = 200000;

        public static void DoCaseA(string[] args)
        {
            try
            {
                //Create list.
                SimpleListContainer container = new SimpleListContainer();
                //Get enumerator.
                SimpleListEnumerator iterator = (SimpleListEnumerator)container.GetEnumerator();

                //Insert count.
                int insert = 0;
                //Delete count.
                int delete = 0;

                //Create random.
                Random random = new Random();
                //Get current clock.
                long start = Millisecond.CurrentTime();
                //Do testing.
                for (int i = 0; i < COUNT; i++)
                {
                    //Get string.
                    string stringValue = (random.Next() % 10000).ToString();

                    //Match flag.
                    bool matched = false;
                    //Reset.
                    iterator.Reset();
                    //Check elements.
                    while (iterator.MoveNext())
                    {
                        //Get element.
                        IListNode element = iterator.Current;
                        //Check object.
                        if (stringValue.Equals(element.GetValue()))
                        {
                            //Set matched.
                            matched = true;
                            //Add delete count.
                            delete ++;
                            //Clear it from list.
                            container.DeleteNode(element);
                        }
                        //Print.
                        //Console.Write("\"" + element.GetValue() + "\",");
                    }
                    //Print.
                    //Console.WriteLine();

                    //Print.
                    //Console.WriteLine("Random string = \"" + string + "\"");
                    
                    //Print information.
                    if ((i + 1) % 10000 == 0)
                    {
                        Console.WriteLine("{0} counts reached !", i + 1);
                    }

                    //Check result.
                    if (!matched)
                    {
                        //Add insert count.
                        insert ++;
                        //Add it into list.
                        if (Even.IsEven(random.Next()))
                        {
                            //Add to head.
                            container.AddHead(stringValue);
                        }
                        else
                        {
                            //Add to tail.
                            container.AddTail(stringValue);
                        }
                    }
                }
                //Calculate consumed time.
                Console.WriteLine("SimpleListContainer.doCaseA : consumed = " + (Millisecond.CurrentTime() - start) + " ms");
                Console.WriteLine("SimpleListContainer.doCaseA : insert = " + insert);
                Console.WriteLine("SimpleListContainer.doCaseA : delete = " + delete);
                Console.WriteLine("SImpleListContainer.doCaseA : size = " + container.GetSize());
                Console.WriteLine("SImpleListContainer.doCaseA : count = " + container.GetCount());
            }
            catch (Exception e)
            {
                Console.WriteLine("SimpleListContainer.doCaseA : " + e.Message);
                Console.WriteLine("SimpleListContainer.doCaseA : unexpected exit !");
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
                Console.WriteLine("SimpleListContainer.main : " + e.Message);
                Console.WriteLine("SimpleListContainer.main : unexpected exit !");
            }
        }
    }
}
