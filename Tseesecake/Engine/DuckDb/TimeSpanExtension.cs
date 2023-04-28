using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tseesecake.Engine.DuckDb
{
    public static class TimeSpanExtension
    {
        public static string ToIntervalDuckDb(this TimeSpan timeSpan)
        {
            var sb = new StringBuilder();
            if (timeSpan.Days > 0)
                sb.Append(timeSpan.Days).Append(" DAY ");
            if (timeSpan.Hours > 0)
                sb.Append(timeSpan.Hours).Append(" HOUR ");
            if (timeSpan.Minutes > 0)
                sb.Append(timeSpan.Minutes).Append(" MINUTE ");
            if (timeSpan.Seconds > 0)
                sb.Append(timeSpan.Seconds).Append(" SECOND ");
            if (timeSpan.Milliseconds > 0)
                sb.Append(timeSpan.Milliseconds).Append(" MILLISECOND ");
#if NET7_0_OR_GREATER
            if (timeSpan.Microseconds > 0)
                sb.Append(timeSpan.Microseconds).Append(" MICROSECOND");
#endif
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            else
                sb.Append("0 MICROSECOND");
            return sb.ToString();
        }
    }
}
