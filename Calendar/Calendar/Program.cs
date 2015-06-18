using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar
{
    internal class Program
    {
        private const int ColumnWidth = 4;
        
        private static readonly List<DayOfWeek> WeekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday, };

        private static readonly ConsoleColor DefaultConsoleColor;

        static Program()
        {
            DefaultConsoleColor = Console.ForegroundColor;
        }

        private static void Main(string[] args)
        {
            var year = GetYear();
            var month = GetMonth();

            PrintDayNames();

            PrintDayNumbers(year, month);
        }

        private static int GetYear()
        {
            int year;
            string yearStr;
            const int minYear = 1900;
            do
            {
                Console.Write("Введите год: ");
                yearStr = Console.ReadLine();
            } while (!int.TryParse(yearStr, out year) || year < minYear);
            return year;
        }

        private static int GetMonth()
        {
            int month;
            string monthStr;
            const int minMonthNumber = 1;
            const int maxMonthNumber = 12;
            do
            {
                Console.Write("Введите месяц: ");
                monthStr = Console.ReadLine();
            } while (!int.TryParse(monthStr, out month) || month < minMonthNumber || month > maxMonthNumber);
            return month;
        }

        private static void PrintDayNames()
        {
            const int dayTitleLength = 2;
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var dayOfWeek in WeekDays)
            {
                Console.Write(dayOfWeek.ToString("G").Substring(0, dayTitleLength).PadLeft(ColumnWidth));
            }

            Console.ForegroundColor = DefaultConsoleColor;
            Console.WriteLine();
        }

        private static void PrintDayNumbers(int year, int month)
        {
            var firstDayDate = new DateTime(year, month, 1);
            var lastDayDate = firstDayDate.AddMonths(1).AddDays(-1);
            var firstDayIndex = WeekDays.IndexOf(firstDayDate.DayOfWeek);
            if (firstDayIndex > 0)
            {
                for (var i = 0; i < firstDayIndex; i++)
                {
                    Console.Write("".PadLeft(ColumnWidth));
                }
            }

            var isTomorrow = false;
            for (var current = firstDayDate; current <= lastDayDate; current = current.AddDays(1))
            {
                Console.ForegroundColor = GetConsoleForegroundColorByDay(current);

                if (current.Date != DateTime.Now.Date)
                {
                    var paddingSpacesWidth = ColumnWidth;
                    if (isTomorrow)
                    {
                        paddingSpacesWidth = ColumnWidth - 1;
                        isTomorrow = false;
                    }
                    Console.Write(current.Day.ToString(CultureInfo.CurrentCulture).PadLeft(paddingSpacesWidth));
                }
                else
                {
                    const ConsoleColor todayColor = ConsoleColor.Red;
                    Console.ForegroundColor = todayColor;
                    Console.Write(("[" + current.Day.ToString(CultureInfo.CurrentCulture)).PadLeft(ColumnWidth));
                    isTomorrow = true;
                    Console.Write("]");
                }

                if (current.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = DefaultConsoleColor;
            Console.WriteLine();
        }

        private static ConsoleColor GetConsoleForegroundColorByDay(DateTime current)
        {
            const ConsoleColor defaultDayColor = ConsoleColor.White;
            const ConsoleColor weekendDayColor = ConsoleColor.Green;
            return current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday ? weekendDayColor : defaultDayColor;
        }
    }
}