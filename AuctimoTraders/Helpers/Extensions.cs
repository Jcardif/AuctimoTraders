using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctimoTraders.Helpers
{
    /// <summary>
    ///     Class that holds extension methods for the Application
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        ///     Convert the provided date to UTC <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetUtcDateTime(this DateTime date) =>
            new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute,
                date.Second, TimeZoneInfo.Local.BaseUtcOffset).UtcDateTime;
    }
}
