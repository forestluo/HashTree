using SimpleTeam.Constant;
using SimpleTeam.Time;

namespace SimpleTeam.Log
{
    public class LogEvent
    {
        //Event description.
        private string value;
        //Event time.
        private SimpleTime time;

        public LogEvent(string value)
        {
            //Set value.
            this.value = value;
            //Use global time.
            time = SimpleLog.GetTime();
        }

        public SimpleTime GetTime()
        {
            //Return time.
            return time;
        }

        public string GetEvent()
        {
            //Return result.
            return value;
        }

        public void SetEvent(string value)
        {
            //Set value.
            this.value = value;
        }
    }
}
