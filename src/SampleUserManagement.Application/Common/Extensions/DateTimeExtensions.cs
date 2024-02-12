using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateOnly? ToDate(this string str, string pattern)
        {
            DateTime date;
            if (DateTime.TryParseExact(str, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return DateOnly.FromDateTime(date);
            }
            return null;
        }

        public static string? ToString(this DateOnly? dateOnly, string pattern)
        {
            return dateOnly?.ToString(pattern);
        }
    }
}
