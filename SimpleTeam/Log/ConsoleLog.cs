namespace SimpleTeam.Log
{
    public class ConsoleLog : ILogListener
    {
        public ConsoleLog()
        {
        
        }

        public void CloseLog()
        {
            //Do nothing.
        }

        public void AcceptLog(LogEvent value)
        {
            //Print.
            Console.Write(value.GetEvent());
    	}
    }
}
