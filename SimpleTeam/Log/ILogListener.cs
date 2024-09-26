namespace SimpleTeam.Log
{
    public interface ILogListener
    {
        public void CloseLog();

        public void AcceptLog(LogEvent value);
    }
}
