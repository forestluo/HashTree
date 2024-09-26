using SimpleTeam.Log;
using SimpleTeam.Constant;

namespace SimpleTeam.Case
{
    internal class LogCase
    {
        //Count.
        private const int COUNT = 10000000;

        public static void DoCaseA(string[] args)
        {
            //Open log.
            SimpleLog.OpenLog(true, false);
            //Log something.
            for (int i = 0; i < COUNT; i++)
            {
                //Log event.
                (new LogString()).Begin("LogCase", Empty.STRING, "DoCaseA").
                    Append("Log something ......").Append(i + 1).End();
                //Print information.
                if ((i + 1) % 1000000 == 0)
                {
                    Console.WriteLine("{0} logs were written !", i + 1);
                }
            }
            //Close log.
            SimpleLog.CloseLog();
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
                Console.WriteLine("LogCase.main : " + e.Message);
                Console.WriteLine("LogCase.main : unexpected exit !");
            }
        }
    }
}