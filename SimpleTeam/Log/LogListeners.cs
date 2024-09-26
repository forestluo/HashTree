namespace SimpleTeam.Log
{
    internal class LogListeners
    {
        private List<ILogListener> listeners;

        public LogListeners()
        {
            //Create vector.
            listeners = new List<ILogListener>();
        }

        public void AddListener(ILogListener listener)
        {
            lock (listeners)
            {
                //Check result.
                if (listeners.IndexOf(listener) < 0) listeners.Add(listener);
            }
        }

        public void RemoveListener(ILogListener listener)
        {
            lock (listeners)
            {
                //Check result.
                if (listeners.IndexOf(listener) >= 0) listeners.Remove(listener);
            }
        }

        public void CloseAll()
        {
            lock (listeners)
            {
                //Close each listener.
                foreach (ILogListener listener in listeners) listener.CloseLog();
                //Clear all.
                listeners.Clear();
            }
        }

        public void Distribute(LogEvent value)
        {
            lock (listeners)
            {
                //Distribute log.
                foreach (ILogListener listener in listeners)
                {
                    try
                    {
                        //Accept log.
                        listener.AcceptLog(value);
                    }
                    catch (Exception e)
                    {
                        //Remove it from list.
                        listeners.Remove(listener);
                        //Log event.
                        Console.WriteLine("LogListeners.Distribute : " + e.Message);
                    }
                }
            }
        }
    }
}
