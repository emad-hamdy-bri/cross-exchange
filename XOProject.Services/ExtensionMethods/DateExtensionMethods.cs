using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace XOProject.Helper.ExtensionMethods
{
    public static class DateExtensionMethods
    {
        public static DateTime GetFirstDayOfWeek(this int year,int week)
        {
            var firstOfYear = new DateTime(year, 1, 1);
            CultureInfo cul = CultureInfo.CurrentCulture;
            while (week != cul.Calendar.GetWeekOfYear(firstOfYear,CalendarWeekRule.FirstDay,DayOfWeek.Monday))
            {
                firstOfYear = firstOfYear.AddDays(1);
            }

            return firstOfYear;
        }

        public static int GetWeekNumber(this DateTime date)
        {
            CultureInfo cul = CultureInfo.CurrentCulture;

            return cul.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
