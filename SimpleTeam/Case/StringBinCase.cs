using System.Text;

using SimpleTeam.Log;
using SimpleTeam.Time;

namespace SimpleTeam.Case
{
    internal class StringBinCase
    {
        //Count.
        private const int COUNT = 100000000;

        public static void DoCaseA(string[] args)
        {
            //Random
            Random random = new Random();

            //Random values.
            int[] randomValues = new int[COUNT];
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Get random.
                randomValues[i] = random.Next();
            }

            //Get current clock.
            long clock1 = DateTime.Now.Ticks;
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Get string value.
                randomValues[i].ToString();
            }
            //Calculate consumed time.
            Console.WriteLine("StringBinCase.main : Integer.ToString consumed = " + (DateTime.Now.Ticks - clock1) / Millisecond.TICKS + " ms");

            //Get current clock.
            long clock2 = DateTime.Now.Ticks;
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Create recycle string.
                StringBuilder buffer = new StringBuilder();
                //Append.
                buffer.Append(randomValues[i]);
                //Get string value.
                buffer.ToString();
            }
            //Calculate consumed time.
            Console.WriteLine("StringBinCase.main : StringBuilder.ToString consumed = " + (DateTime.Now.Ticks - clock2) / Millisecond.TICKS + " ms");

            //Get current clock.
            long clock3 = DateTime.Now.Ticks;
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Create recycle string.
                StringBuilder buffer = new StringBuilder();
                //Append.
                buffer.Append(randomValues[i]);
                //Get string value.
                buffer.ToString();
            }
            //Calculate consumed time.
            Console.WriteLine("StringBinCase.main : StringBuilder.ToString consumed = " + (DateTime.Now.Ticks - clock3) / Millisecond.TICKS + " ms");

            //Get current clock.
            long clock4 = DateTime.Now.Ticks;
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Create recycle string.
                RecycleString buffer = new RecycleString();
                //Append.
                buffer.Append(randomValues[i]);
                //Get string value.
                buffer.ToString();
            }
            //Calculate consumed time.
            Console.WriteLine("StringBinCase.main : RecycleString.ToString consumed = " + (DateTime.Now.Ticks - clock4) / Millisecond.TICKS + " ms");

            //Get current clock.
            long clock5 = DateTime.Now.Ticks;
            //Do testing.
            for (int i = 0; i < COUNT; i++)
            {
                //Create log string.
                LogString buffer = new LogString();
                //Append.
                buffer.Append(randomValues[i]);
                //Get string.
                buffer.ToString();
            }
            //Calculate consumed time.
            Console.WriteLine("StringBinCase.main : LogString.ToString consumed = " + (DateTime.Now.Ticks - clock5) / Millisecond.TICKS + " ms");
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
                Console.WriteLine("StringBinCase.main : " + e.Message);
                Console.WriteLine("StringBinCase.main : unexpected exit !");
            }
        }
    }
}
