using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_10_Upcasting_Downcasting.Classes
{
    internal static class HelperStaticClass
    {
        public static bool NameValidation(string name)
        {
            if (string.IsNullOrEmpty(name) || name.All(char.IsWhiteSpace))
                return false;
            else
                return true;
        }

        public static bool PriceValidation(int price)
        {
            if (price > 0) return true;
            else return false;
        }

        public static bool PersonCapacity(int count)
        {
            if (count>0) return true;
            else return false;
        }

        public static void ColoringText(string message, ConsoleColor color=ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();

        }

    }
}
