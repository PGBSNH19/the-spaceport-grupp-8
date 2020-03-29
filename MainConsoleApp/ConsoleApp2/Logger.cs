using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    public class Logger
    {
        public static void systemLog(string s, ConsoleColor consoleColorEnum = ConsoleColor.Red) // default red
        {
            Console.ForegroundColor = consoleColorEnum;
            Console.WriteLine(($"-[{s.ToUpper()}]-"));
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
