using SimpleTeam.Log;

namespace SimpleTeam.Constant.Copywrite
{
    public class SystemTitle
    {
        public static void PrintTitle(StringWriter sw, string title)
        {
            sw.WriteLine("-------------------------------------------------------------------------------");

            //Calculate count of blank space.
            int count = (77 - title.Length) / 2;
            //Print left.
            sw.Write("|");
            for (int i = 0; i < count; i++) sw.Write(" ");
            //Print title.
            sw.Write(title);
            //Print right.
            for (int i = count + title.Length + 1; i < 78; i++) sw.Write(" ");
            sw.WriteLine("|");

            sw.WriteLine("-------------------------------------------------------------------------------");
        }

        public static void PrintBanner(string title)
        {
            Console.WriteLine("-------------------------------------------------------------------------------");

            //Calculate count of blank space.
            int count = (77 - title.Length) / 2;
            //Print left.
            Console.Write("|");
            for (int i = 0; i < count; i++) Console.Write(" ");
            //Print title.
            Console.Write(title);
            //Print right.
            for (int i = count + title.Length + 1; i < 78; i++) Console.Write(" ");
            Console.WriteLine("|");

            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public static void PrintVersion(StringWriter sw, string version)
        {
            PrintTitle(sw, version);

            sw.WriteLine("|-----------------------------------------------------------------------------|");
            sw.WriteLine("|                                 SimpleTeam                                  |");
            sw.WriteLine("|-----------------------------------------------------------------------------|");
            sw.WriteLine("| Supported by :   www.simpleteam.com  |  E-Mail :        forestluo@gmail.com |");
            sw.WriteLine("|-----------------------------------------------------------------------------|");
        }

        public static void PrintTitle(string title)
        {
            Console.WriteLine("-------------------------------------------------------------------------------");

            //Calculate count of blank space.
            int count = (77 - title.Length) / 2;
            //Print left.
            Console.Write("|");
            for (int i = 0; i < count; i++) Console.Write(" ");
            //Print title.
            Console.Write(title);
            //Print right.
            for (int i = count + title.Length + 1; i < 78; i++) Console.Write(" ");
            Console.WriteLine("|");

            Console.WriteLine("|-----------------------------------------------------------------------------|");
            Console.WriteLine("|                                 SimpleTeam                                  |");
            Console.WriteLine("|-----------------------------------------------------------------------------|");
            Console.WriteLine("| Supported by :   www.simpleteam.com  |  E-Mail :        forestluo@gmail.com |");
            Console.WriteLine("|-----------------------------------------------------------------------------|");
        }

        public static string FormatTitle(string title)
        {
            //Create string writer.
            StringWriter sw = new StringWriter();

            sw.WriteLine("-------------------------------------------------------------------------------");

            //Calculate count of blank space.
            int count = (77 - title.Length) / 2;
            //Print left.
            sw.Write("|");
            for (int i = 0; i < count; i++) sw.Write(' ');
            //Print title.
            sw.Write(title);
            //Print right.
            for (int i = count + title.Length + 1; i < 78; i++) sw.Write(' ');
            sw.WriteLine("|");

            sw.WriteLine("|-----------------------------------------------------------------------------|");
            sw.WriteLine("|                                 SimpleTeam                                  |");
            sw.WriteLine("|-----------------------------------------------------------------------------|");
            sw.WriteLine("| Supported by :   www.simpleteam.com  |  E-Mail :        forestluo@gmail.com |");
            sw.WriteLine("|-----------------------------------------------------------------------------|");
            //Return result.
            return sw.ToString();
        }
    }
}
