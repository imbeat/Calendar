using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar
{
    internal class Program
    {
        private const int ColumnWidth = 4;

        private static readonly List<DayOfWeek> WeekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday, };

        private static void Main(string[] args)
        {
            var year = GetYear();
            var month = GetMonth();

            PrintDayNames();

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

            var needToAddClosingBrace = false;
            for (var current = firstDayDate; current <= lastDayDate; current = current.AddDays(1))
            {
                if (current.Date != DateTime.Now.Date)
                {
                    var paddingSpacesWidth = ColumnWidth;
                    if (needToAddClosingBrace)
                    {
                        paddingSpacesWidth = ColumnWidth - 1;
                        needToAddClosingBrace = false;
                    }
                    Console.Write(current.Day.ToString(CultureInfo.CurrentCulture).PadLeft(paddingSpacesWidth));
                }
                else
                {
                    Console.Write(("[" + current.Day.ToString(CultureInfo.CurrentCulture)).PadLeft(ColumnWidth));
                    needToAddClosingBrace = true;
                    Console.Write("]");
                }

                if (current.DayOfWeek == DayOfWeek.Sunday)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        private static void PrintDayNames()
        {
            foreach (var dayOfWeek in WeekDays)
            {
                Console.Write(dayOfWeek.ToString("G").Substring(0, 2).PadLeft(ColumnWidth));
            }
            Console.WriteLine();
        }

        private static int GetMonth()
        {
            int month;
            string monthStr;
            do
            {
                Console.Write("Введите месяц: ");
                monthStr = Console.ReadLine();
            } while (!int.TryParse(monthStr, out month) || month < 1 || month > 12);
            return month;
        }

        private static int GetYear()
        {
            int year;
            string yearStr;
            do
            {
                Console.Write("Введите год: ");
                yearStr = Console.ReadLine();
            } while (!int.TryParse(yearStr, out year) || year < 1900);
            return year;
        }
    }
}