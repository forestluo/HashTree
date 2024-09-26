using SimpleTeam.Time;

namespace SimpleTeam.Case
{
    internal class SystemTimeCase
    {
        public static void DoCaseA(string[] args)
        {
            //Get ticks.
            long ticks = DateTime.Now.Ticks;
            //Print out.
            Console.WriteLine("System Time Ticks = {0} ({1}ms) ", ticks, ticks / 10000);
            //Print time.
            Console.WriteLine("System Time : {0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());

            //Get current milliseconds.
            Console.WriteLine("Current Timestamp in Milliseconds = {0} milliseconds", Millisecond.CurrentTime());
            //Get current seconds
            Console.WriteLine("Current Timestamp in Seconds = {0} seconds", Second.CurrentTime());
            //Get current minutes.
            Console.WriteLine("Current Timestamp in Minutes = {0} minutes", Minute.CurrentTime());
            //Get current hours.
            Console.WriteLine("Current Timestamp in Hours = {0} hours", Hour.CurrentTime());
            //Get current days.
            Console.WriteLine("Current Timestamp in Days = {0} days", Day.CurrentTime());

            //Get time of a day.
            Console.WriteLine("Current Date Time = {0}-{1}-{2} {3} {4}:{5}:{6}.{7}",
                Year.GetYear(ticks),
                Month.GetMonth(ticks), Day.GetDay(ticks),Week.GetWeek(ticks),
                Hour.GetHourOfDay(ticks), Minute.GetMinuteOfDay(ticks),
                Second.GetSecondOfDay(ticks), Millisecond.GetMillisecondOfDay(ticks));

            SimpleTime time = new SimpleTime(ticks);
            //Print compact format.
            Console.WriteLine("Date Format = {0}", time.DateFormat("-"));
            Console.WriteLine("Time Format = {0}", time.TimeFormat(":"));
            Console.WriteLine("Compact Format = {0}", time.CompactFormat());
            Console.WriteLine("Standard Format = {0}", time.StandardFormat());
            //Try parse.
            if (!time.CompactParse(time.CompactFormat()))
                Console.WriteLine("Fail to do compact parse !");
            else Console.WriteLine("Success to do compact parse !");
            if (!time.StandardParse(time.StandardFormat()))
                Console.WriteLine("Fail to do standard parse !");
            else Console.WriteLine("Success to do standard parse !");
            if (!time.CompactParse(time.StandardFormat()))
                Console.WriteLine("Fail to do standard parse !");
            else Console.WriteLine("Success to do standard parse !");
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
                Console.WriteLine("SystemTimeCase.main : " + e.Message);
                Console.WriteLine("SystemTimeCase.main : unexpected exit !");
            }
        }
    }
}
