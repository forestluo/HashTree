using SimpleTeam.Constant;

namespace SimpleTeam.Log
{
    public class LogString : RecycleString
    {
        //Recyclable
        private readonly bool recyclable;

        public LogString(bool recyclable = false)
        {
            //Set flag.
            this.recyclable = recyclable;
        }

        public LogString Begin(string className = Empty.STRING,
            string instanceName = Empty.STRING, string functionName = Empty.STRING)
        {
            //Append class name.
            AppendClass(className).
                AppendInstance(instanceName).
                    AppendFunction(functionName).Append(" : ");
            //Return result.
            return this;
        }

        public override void End()
        {
            //End line.
            base.End();
            //Return result.
            SimpleLog.Log(recyclable ? LogBin.Recycle(this) : ToString());
        }

        private LogString AppendClass(string className)
        {
            //Check class name.
            return (className != null &&
                className.Length > 0) ?
                (LogString)Append(className) : this;
        }

        private LogString AppendInstance(string instanceName)
        {
            //Check class name.
            return (instanceName != null && instanceName.Length > 0) ?
                (LogString)Append('(').Append(instanceName).Append(')') : this;
        }

        private LogString AppendFunction(string functionName)
        {
            //Check class name.
            return (functionName != null &&
                functionName.Length > 0) ?
                (LogString)Append('.').Append(functionName) : this;
        }

        public LogString Append(Exception exception)
        {
            //Return result.
            return Append(exception, false);
        }

        public LogString Append(Exception exception, bool printStack)
        {
            //Add message.
            Append("[e] ").NewLine(exception.Message);
            //Chechk flag.
            if (printStack)
            {
                try
                {
                    //Print stack trace.
                    if (exception.StackTrace != null)
                    {
                        //Write stack trace.
                        NewLine(exception.StackTrace);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("LogString.exception : " + e.Message);
                    Console.WriteLine("LogString.exception : unexpected exit !");
                }
            }
            //Return this.
            return this;
        }
    }
}
