using System;
using System.Globalization;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Chart
{
    internal static class ChartDataRows
    {
        internal static string Name(MonthlyValues[] source, string key)
        {
            return Array.Find(source, item => item.Key == key)?.Name;
        }

        internal static ChartRow[] Totals(MonthlyValues[] source)
        {
            return source.Select(item => new ChartRow(item.Key, item.Name, item.Values.Sum())).ToArray();
        }

        internal static ChartRow[] Trend(MonthlyValues[] selected)
        {
            if (selected.Length == 0)
            {
                return new ChartRow[0];
            }

            return Enumerable.Range(0, 6)
                .Select(index => new ChartRow("2026-" + (index + 4).ToString("00", CultureInfo.InvariantCulture),
                    (index + 4) + " 月", selected.Sum(item => item.Values[index])))
                .ToArray();
        }

        internal sealed class MonthlyValues
        {
            public MonthlyValues(string key, string name, params int[] values)
            {
                Key = key;
                Name = name;
                Values = values;
            }

            public string Key { get; }
            public string Name { get; }
            public int[] Values { get; }
        }

        internal sealed class ChartRow
        {
            public ChartRow(string key, string category, int amount)
            {
                Key = key;
                Category = category;
                Amount = amount;
            }

            public string Key { get; }
            public string Category { get; }
            public int Amount { get; }
        }
    }
}
