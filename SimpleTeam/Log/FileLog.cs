using SimpleTeam.IO;
using SimpleTeam.Time;

namespace SimpleTeam.Log
{
    public class FileLog
        : ILogListener
    {
        //Stream writer.
        private StreamWriter sw;

        //Date
        private int openDate;
        //Open time of accepter.
        private SimpleTime openTime;

        public FileLog()
        {
            //Get date value.
            openDate = (openTime = SimpleLog.GetTime()).GetDate();
            //Open file.
            sw = new StreamWriter(SimpleLog.DateFormat() + ".log", true);
        }

        public void CloseLog()
        {
            try
            {
                //Flush
                sw.Flush();
                //Close current file
                sw.Close();
                //Print information
                Console.WriteLine("FileLog.CloseLog : file log closed !");
            }
            catch (Exception e)
            {
                throw new Exception("FileLog.CloseLog : unexpected exit !", e);
            }
        }

        public void AcceptLog(LogEvent value)
        {
            //Get time.
            SimpleTime time = value.GetTime();

            try
            {
                //Compare current date.
                if (time.GetDate() != openDate)
                {
                    //Close current file.
                    sw.Close();
                    //Set date value.
                    openDate = (openTime = time).GetDate();
                    //Open file.
                    sw = new StreamWriter(SimpleLog.DateFormat() + ".log", true);
                }
                //Print multiline.
                Printer.PrintMultiline(sw, SimpleLog.TimeFormat() + ">", value.GetEvent());
		    }
		    catch(Exception e)
		    {
			    throw new Exception("FileLog.AcceptLog : unexpected exit !", e);
            }
	    }
    }
}
