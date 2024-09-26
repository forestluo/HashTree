using System.Timers;

using SimpleTeam.Time;
using SimpleTeam.Constant.Copywrite;

namespace SimpleTeam.Log
{
    public class SimpleLog
    {
        //Class Name
        private const string CLASS_NAME = "SimpleLog";

        //Listeners
        private static LogListeners listeners;

        //Event time.
        private static SimpleTime logTime;
        //Formats.
        private static string dateFormat;
        private static string timeFormat;
        //Log timer.
        private static System.Timers.Timer logTimer;

        static SimpleLog()
        {
            //Set default time.
            logTime = new SimpleTime();
            dateFormat = logTime.DateFormat("-");
            timeFormat = logTime.TimeFormat(":");
            //Create listeners.
            listeners = new LogListeners();

            //Create a timer with an one second interval.
            logTimer = new System.Timers.Timer(Second.VALUE);
#pragma warning disable CS8622 // 参数类型中引用类型的为 Null 性与目标委托不匹配(可能是由于为 Null 性特性)。
            // Hook up the Elapsed event for the timer. 
            logTimer.Elapsed += OnTimedEvent; logTimer.AutoReset = true; logTimer.Enabled = true;
#pragma warning restore CS8622 // 参数类型中引用类型的为 Null 性与目标委托不匹配(可能是由于为 Null 性特性)。
        }

        internal static string DateFormat()
        {
            lock(logTime)
            {
                //Return result.
                return dateFormat;
            }
        }

        internal static string TimeFormat()
        {
            lock(logTime)
            {
                //Return result.
                return timeFormat;
            }
        }

        internal static SimpleTime GetTime()
        {
            //Return result.
            return logTime;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            lock(logTime)
            {
                //Set date time.
                logTime = new SimpleTime(e.SignalTime);
                //Update formats.
                dateFormat = logTime.DateFormat("-");
                timeFormat = logTime.TimeFormat(":");
            }
        }

        public static void Log(string value)
        {
            //Log event.
            Log(new LogEvent(value));
        }

        public static void Log(LogEvent value)
        {
            try
            {
                //Check log file.
                listeners.Distribute(value);
            }
		    catch(Exception e)
		    {
			    Console.WriteLine("SimpleLog.log : " + e.Message);
                Console.WriteLine("SimpleLog.log : unexpected exit !");
            }
        }

        public static void CloseLog()
        {
            //Dispose timer.
            logTimer.Stop();
            logTimer.Dispose();
            //Close all logs.
            listeners.CloseAll();
        }

        public static void OpenLog(bool fileLog = true, bool consoleLog = true)
        {
            //Add listener.
            if (fileLog) listeners.AddListener(new FileLog());
            //Add listener.
            if (consoleLog) listeners.AddListener(new ConsoleLog());
            //Log title.
            SimpleLog.Log(SystemTitle.FormatTitle("Kernel " + SimpleTeam.Constant.Copywrite.Version.STRING));
        }
    }
}
