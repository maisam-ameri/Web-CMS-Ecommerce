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
        
        public static string MiladiToShamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime persianDate = date;
            var year = persianCalendar.GetYear(persianDate);
            var month = persianCalendar.GetMonth(persianDate);
            var day = persianCalendar.GetDayOfMonth(persianDate);
            return $"{year}/{month}/{day}";
        }
    }
}
