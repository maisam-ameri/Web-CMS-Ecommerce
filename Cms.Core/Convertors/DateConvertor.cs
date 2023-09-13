using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Convertors
{
    public static class DateConvertor
    {
        
        public static string MiladiToShamsi(this string date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime dateTime = DateTime.Parse(date);
            var year = persianCalendar.GetYear(dateTime);
            var month = persianCalendar.GetMonth(dateTime);
            var day = persianCalendar.GetDayOfMonth(dateTime);
            return $"{year}/{month}/{day}";
        }
    }
}
