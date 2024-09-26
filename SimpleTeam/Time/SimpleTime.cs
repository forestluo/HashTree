using SimpleTeam.Constant;

namespace SimpleTeam.Time
{
    public class SimpleTime
    {
        //Date time.
        private DateTime dateTime;

        public SimpleTime()
        {
            //表示自01年1月1日午夜12:00:00
            //Set current time.
            dateTime = DateTime.Now;
        }

        public SimpleTime(long ticks)
        {
            //Set time in ticks.
            dateTime = new DateTime(ticks);
        }

        public SimpleTime(DateTime dateTime)
        {
            //Set time in ticks.
            this.dateTime = dateTime;
        }

        public long GetTicks()
        {
            //Get time in ticks.
            return dateTime.Ticks;
        }

        public void Reset()
        {
            //Set current time.
            dateTime = DateTime.Now;
        }

        public void Reset(long ticks)
        {
            //Set current time.
            dateTime = new DateTime(ticks);
        }

        public string CompactFormat()
        {
            //Return result.
            return dateTime.ToString("yyyyMMddHHmmss");
        }

        public bool CompactParse(string value)
        {
            return Parse(value, "yyyyMMddHHmmss");
        }

        public string StandardFormat()
        {
            //Return result.
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public bool StandardParse(string value)
        {
            return Parse(value, "yyyy-MM-dd HH:mm:ss");
        }

        public string DateFormat(string splitter = Empty.STRING)
        {
            //Return result.
            if (string.IsNullOrEmpty(splitter))
                //Return result.
                return dateTime.ToString("yyyyMMdd");
            //Format string.
            return dateTime.ToString(string.Format("yyyy{0}MM{0}dd", splitter));
        }

        public string TimeFormat(string splitter = Empty.STRING)
        {
            //Return result.
            if (string.IsNullOrEmpty(splitter))
                //Return result.
                return dateTime.ToString("HHmmss");
            //Format string.
            return dateTime.ToString(string.Format("HH{0}mm{0}ss", splitter));
        }

        public string Format(string format)
        {
            //Return result.
            return dateTime.ToString(format);
        }

        public bool Parse(string value, string format)
        {
            try
            {
                //Try parse.
                dateTime = DateTime.ParseExact(value, format, null);
                //Return true.
                return true;
            }
            catch (Exception e)
            {
                //Print.
                Console.WriteLine("SimpleTime.Parse : " + e.Message);
                Console.WriteLine("SimpleTime.Parse : unexpected exit !");
            }
            //Return false.
            return false;
        }

        public long GetTimeInMillis()
        {
            //Return result.
            return dateTime.Ticks / Millisecond.TICKS;
        }

        public void SetTimeInMillis(long millisecond)
        {
            //Set millisecond.
            dateTime = new DateTime(millisecond * Millisecond.TICKS);
        }

        public int GetYear()
        {
            //Return result.
            return dateTime.Year;
        }

        public int GetMonth()
        {
            //Return result.
            return dateTime.Month;
        }

        public int GetDate()
        {
            //Return result.
            return dateTime.Day;
        }

        public int GetHour()
        {
            //Return result.
            return dateTime.Hour;
        }

        public int GetMinute()
        {
            //Return result.
            return dateTime.Minute;
        }

        public int GetSecond()
        {
            //Return result.
            return dateTime.Second;
        }

        public int GetMillisecond()
        {
            //Return result.
            return dateTime.Millisecond;
        }

        public void SetYear(int year)
        {
            //Set value.
            dateTime = new DateTime(year,
                dateTime.Month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public void SetMonth(int month)
        {
            //Set value.
            dateTime = new DateTime(dateTime.Year,
                month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public void SetDate(int date)
        {
            //Set value.
            dateTime = new DateTime(dateTime.Year,
                dateTime.Month, date,
                dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public void SetHour(int hour)
        {
            //Set value.
            dateTime = new DateTime(dateTime.Year,
                dateTime.Month, dateTime.Day,
                hour, dateTime.Minute, dateTime.Second);
        }

        public void SetMinute(int minute)
        {
            //Set value.
            dateTime = new DateTime(dateTime.Year,
                dateTime.Month, dateTime.Day,
                dateTime.Hour, minute, dateTime.Second);
        }

        public void SetSecond(int second)
        {
            //Set value.
            dateTime = new DateTime(dateTime.Year,
                dateTime.Month, dateTime.Day,
                dateTime.Hour, dateTime.Minute, second);
        }

        public void SetDateTime(int year,
            int month, int day, int hour, int minute, int second)
        {
            //Set value.
            dateTime = new DateTime(year, month, day, hour, minute, second);
        }
    }
}
