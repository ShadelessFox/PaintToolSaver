using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI_Autosaver.Core
{
    static class DateUtil
    {
        public static string PluralForm(int num, string[] words)
        {
            int n0 = Math.Abs(num) % 100;
            int n1 = n0 % 10;
            return (10 < n0 && n0 < 20) ? words[2] : (1 < n1 && n1 < 5) ? words[1] : (n1 == 1) ? words[0] : words[2];
        }

        public static string FormatDate(TimeSpan span, string[] hoursFormat, string[] minutesFormat, string[] secondsFormat)
        {
            var items = new List<string>();
            if (span.Hours > 0)
                items.Add($"{span.Hours} {PluralForm(span.Hours, hoursFormat)}");
            if (span.Minutes > 0)
                items.Add($"{span.Minutes} {PluralForm(span.Minutes, minutesFormat)}");
            if (span.Seconds > 0)
                items.Add($"{span.Seconds} {PluralForm(span.Seconds, secondsFormat)}");
            return String.Join(" ", items);
        }
    }
}
