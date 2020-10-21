using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugMania.Helpers
{
    public static class DateTimeHelpers
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        public static string GetRelativeTimePassed(DateTime dateTime, string prefix = "Posted")
        {
            if (dateTime == null)
            {
                return String.Empty;
            }

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            prefix = prefix + " ";

            if (delta < 1 * MINUTE)
                return prefix + (ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago");

            if (delta < 2 * MINUTE)
                return prefix + "a minute ago";

            if (delta < 45 * MINUTE)
                return prefix + ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return prefix + "an hour ago";

            if (delta < 24 * HOUR)
                return prefix + ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return prefix + "yesterday";

            if (delta < 30 * DAY)
                return prefix + ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return prefix + (months <= 1 ? "one month ago" : months + " months ago");
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return prefix + (years <= 1 ? "one year ago" : years + " years ago");
            }
        }

        public static DateTime? getLocalTime(DateTime? timeToConvert)
        {
            if (timeToConvert.HasValue)
            {
                var ticks = timeToConvert.Value.Ticks + (DateTime.Now.Ticks - DateTime.UtcNow.Ticks);
                return new DateTime(ticks);
            }
            else
            {
                return null;
            }
        }
    }   
}