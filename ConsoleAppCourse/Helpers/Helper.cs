using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCourse.Helpers
{
   public static class Helper
    {
        public static void PrintConsole(ConsoleColor color,string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }
    }
}
