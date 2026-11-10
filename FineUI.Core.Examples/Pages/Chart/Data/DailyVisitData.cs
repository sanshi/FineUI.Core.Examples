using System;
using System.Globalization;
using System.Linq;
using static FineUI.Core.Examples.Pages.Chart.ChartDataRows;

namespace FineUI.Core.Examples.Pages.Chart
{
    internal static class DailyVisitData
    {
        internal const string FirstDate = "2026-09-01";

        internal const string LastDate = "2026-09-12";

        private static readonly int[] Visits = { 1120, 1240, 1180, 1390, 1480, 980, 890, 1410, 1520, 1470, 1660, 1570 };

        private static readonly int[] Conversions = { 58, 71, 64, 82, 92, 41, 36, 84, 96, 87, 105, 99 };

        internal static ChartRow[] DailyVisits(string start = FirstDate, string end = LastDate)
        {
            if (!TryReadDate(start, out var first) || !TryReadDate(end, out var last) || first > last)
            {
                return null;
            }

            var firstDay = new DateTime(2026, 9, 1);
            return Visits.Select((value, index) => new { Date = firstDay.AddDays(index), Value = value })
                .Where(day => day.Date >= first && day.Date <= last)
                .Select(day => new ChartRow(day.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    day.Date.ToString("MM-dd", CultureInfo.InvariantCulture), day.Value))
                .ToArray();
        }

        internal static VisitDetails FindVisit(string key)
        {
            if (!TryReadDate(key, out var date))
            {
                return null;
            }

            int index = (date - new DateTime(2026, 9, 1)).Days;
            return index >= 0 && index < Visits.Length ? new VisitDetails(Visits[index], Conversions[index]) : null;
        }

        private static bool TryReadDate(string value, out DateTime date)
        {
            return DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        internal sealed class VisitDetails
        {
            public VisitDetails(int visits, int conversions)
            {
                Visits = visits;
                Conversions = conversions;
            }

            public int Visits { get; }
            public int Conversions { get; }
        }
    }
}
