using System;

namespace Calendar
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int year;
            string yearStr;
            do
            {
                Console.Write("Введите год: ");
                yearStr = Console.ReadLine();
            } while (!int.TryParse(yearStr, out year) || year < 1900);

            int month;
            string monthStr;
            do
            {
                Console.Write("Введите месяц: ");
                monthStr = Console.ReadLine();
            } while (!int.TryParse(monthStr, out month) || month < 1 || month > 12);
        }
    }
}