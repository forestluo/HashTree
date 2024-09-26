using System.Text;

using SimpleTeam.Constant;

namespace SimpleTeam.IO
{
    public class Printer
    {
        public static void PrintMultiline(StreamWriter sw, string prefix, string value)
        {
            //Position
            int position = 0;
            //Do while.
            do
            {
                //Print prefix.
                sw.Write(prefix);
                //Do while.
                while (position < value.Length)
                {
                    //Get char.
                    char charValue = value[position];
                    //Write char value.
                    sw.Write(charValue); position++;
                    //Check char value.
                    if (charValue == LF.VALUE) break;
                }
                //Do while.
            } while (position < value.Length);
        }

        public static void PrintMultiline2(StreamWriter sw, string prefix, string value)
        {
            //Create buffer.
            StringBuilder buffer = new StringBuilder();
            //Position
            int position = 0;
            //Do while.
            do
            {
                //Print prefix.
                buffer.Append(prefix);
                //Do while.
                while (position < value.Length)
                {
                    //Get char.
                    char charValue = value[position];
                    //Write char value.
                    buffer.Append(charValue); position ++;
                    //Check char value.
                    if (charValue == LF.VALUE) break; 
                }
                //Do while.
            } while(position < value.Length);
            //Write out.
            sw.Write(buffer.ToString());
        }

        public static void PrintMultiline3(StreamWriter sw, string prefix, string value)
        {
            //Position.
            int position = 0;
            //Set end position.
            int endPosition = 0;
            //Do while.
            while (endPosition < value.Length)
            {
                //Get char.
                char charValue = value[endPosition];
                //Check char.
                if (charValue == CR.VALUE || charValue == LF.VALUE)
                {
                    //Check end position.
                    if (endPosition > position)
                    {
                        //Print prefix.
                        sw.Write(prefix);
                        //Print event.
                        sw.WriteLine(value.Substring(position, endPosition - position + 1));
                    }
                    //Set new position.
                    position = endPosition + 1;
                }
                //Add end position.
                endPosition++;
            }
            //Check end position.
            if (endPosition > position)
            {
                //Print prefix.
                sw.Write(prefix);
                //Print event.
                sw.WriteLine(value.Substring(position, endPosition - position + 1));
            }
        }
    }
}
