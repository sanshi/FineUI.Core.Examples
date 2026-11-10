using System;
using System.Globalization;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Chart
{
    internal static class ChartInteractionData
    {
        internal const string FirstDate = "2026-09-01";
        internal const string LastDate = "2026-09-12";

        private static readonly MonthlyValues[] Regions =
        {
            new MonthlyValues("east", "华东", 6800, 7400, 7100, 8900, 9200, 10100),
            new MonthlyValues("north", "华北", 5400, 6200, 5800, 7100, 7700, 8200),
            new MonthlyValues("south", "华南", 5900, 6300, 6900, 7200, 7900, 8800),
        };

        private static readonly ChartRow[][] Cities =
        {
            new[]
            {
                new ChartRow("shanghai", "上海", 21800),
                new ChartRow("suzhou", "苏州", 14600),
                new ChartRow("hangzhou", "杭州", 13100),
            },
            new[]
            {
                new ChartRow("beijing", "北京", 17100),
                new ChartRow("tianjin", "天津", 12200),
                new ChartRow("jinan", "济南", 11100),
            },
            new[]
            {
                new ChartRow("guangzhou", "广州", 18200),
                new ChartRow("shenzhen", "深圳", 14900),
                new ChartRow("foshan", "佛山", 9900),
            },
        };

        private static readonly int[] Visits = { 1120, 1240, 1180, 1390, 1480, 980, 890, 1410, 1520, 1470, 1660, 1570 };
        private static readonly int[] Conversions = { 58, 71, 64, 82, 92, 41, 36, 84, 96, 87, 105, 99 };

        private static readonly MonthlyValues[] Channels =
        {
            new MonthlyValues("search", "搜索", 2900, 3200, 3140, 3470, 3890, 4120),
            new MonthlyValues("direct", "直接访问", 1280, 1400, 1530, 1550, 1640, 1890),
            new MonthlyValues("referral", "外部链接", 920, 1010, 970, 1100, 1260, 1210),
            new MonthlyValues("campaign", "推广活动", 640, 950, 710, 1230, 1380, 1190),
        };

        internal static ChartRow[] RegionTotals() => Totals(Regions);

        internal static ChartRow[] ChannelTotals() => Totals(Channels);

        internal static string RegionName(string key) => Name(Regions, key);

        internal static string ChannelName(string key) => Name(Channels, key);

        internal static ChartRow[] RegionTrend(string key)
        {
            return Trend(Regions.Where(region => region.Key == key).ToArray());
        }

        internal static ChartRow[] RegionTrend(string[] keys)
        {
            return Trend(Regions.Where(region => keys != null && keys.Contains(region.Key)).ToArray());
        }

        internal static ChartRow[] CityTotals(string regionKey)
        {
            var index = Array.FindIndex(Regions, region => region.Key == regionKey);
            return index < 0 ? new ChartRow[0] : Cities[index];
        }

        internal static ChartRow[] ChannelTrend(string key = null)
        {
            return Trend(Channels.Where(channel => key == null || channel.Key == key).ToArray());
        }

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

        internal static string VisitSummary(ChartRow[] rows)
        {
            return rows.Length == 0 ? "所选日期内没有访问数据" : $"{rows.Length} 天 · 访问量 {rows.Sum(row => row.Amount)}";
        }

        internal static string AreaSummary(ChartRow[] rows)
        {
            return rows.Length == 0 ? "请选择至少一个区域" : $"所选区域销售额合计 {rows.Sum(row => row.Amount)} 元";
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

        internal static ChartRow[] RegionThreshold(string minimum)
        {
            if (!int.TryParse(minimum, NumberStyles.None, CultureInfo.InvariantCulture, out int value))
            {
                return null;
            }

            return RegionTotals().Where(row => row.Amount >= value).OrderByDescending(row => row.Amount).ToArray();
        }

        internal static string ThresholdSummary(ChartRow[] rows)
        {
            if (rows == null)
            {
                return "请输入非负整数金额";
            }
            return rows.Length == 0 ? "没有达到门槛的区域" : $"符合门槛的区域：{rows.Length} 个";
        }

        internal static ChartRow[] DailyMetric(string metric)
        {
            var rows = DailyVisits();
            return metric == "conversions"
                ? rows.Select((row, index) => new ChartRow(row.Key, row.Category, Conversions[index])).ToArray()
                : rows;
        }

        internal static string MetricSummary(string metric, ChartRow[] rows)
        {
            var name = metric == "conversions" ? "转化量" : "访问量";
            return $"{name}合计：{rows.Sum(row => row.Amount)}";
        }

        internal static object DailyFocus(string key)
        {
            return DailyVisits().Select(row => new
            {
                row.Key,
                row.Category,
                row.Amount,
                Color = row.Key == key ? "#d9480f" : ""
            }).ToArray();
        }

        internal static string VisitFocusSummary(string key)
        {
            var details = FindVisit(key);
            return details == null ? "所选日期没有访问数据" : $"{key} · 访问量 {details.Visits} · 转化量 {details.Conversions}";
        }

        internal static ChartRow[] AreaAmounts(bool cumulative)
        {
            var rows = Trend(Regions);
            if (!cumulative)
            {
                return rows;
            }

            // 截至当月包含之前各月。比如 4 月为 18100，5 月的累计值再加上 5 月的 19900。
            int total = 0;
            return rows.Select(row =>
            {
                total += row.Amount;
                return new ChartRow(row.Key, row.Category, total);
            }).ToArray();
        }

        internal static string AreaModeSummary(bool cumulative)
        {
            return cumulative ? "统计口径：截至当月累计" : "统计口径：本月金额";
        }

        internal static ChartRow[] ChannelDistribution(string month)
        {
            if (month == "all")
            {
                return ChannelTotals();
            }

            int index = Array.FindIndex(ChannelTrend(), row => row.Key == month);
            return index < 0 ? new ChartRow[0]
                : Channels.Select(channel => new ChartRow(channel.Key, channel.Name, channel.Values[index])).ToArray();
        }

        internal static string DistributionSummary(string month, ChartRow[] rows)
        {
            var range = month == "all" ? "全部月份" : Array.Find(ChannelTrend(), row => row.Key == month)?.Category;
            return $"{range} · 总访问量 {rows.Sum(row => row.Amount)}";
        }

        internal static string ChannelDetailsSummary(string key, ChartRow[] rows)
        {
            return key == null ? "请选择一个渠道" : $"{ChannelName(key)} · 六个月访问量 {rows.Sum(row => row.Amount)}";
        }

        private static bool TryReadDate(string value, out DateTime date)
        {
            return DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        private static string Name(MonthlyValues[] source, string key)
        {
            return Array.Find(source, item => item.Key == key)?.Name;
        }

        private static ChartRow[] Totals(MonthlyValues[] source)
        {
            return source.Select(item => new ChartRow(item.Key, item.Name, item.Values.Sum())).ToArray();
        }

        private static ChartRow[] Trend(MonthlyValues[] selected)
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

        private sealed class MonthlyValues
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

        internal sealed class VisitDetails
        {
            public VisitDetails(int visits, int conversions)
            {
                Visits = visits;
                Conversions = conversions;
            }

            public int Visits { get; }
            public int Conversions { get; }
            public string Rate => (Conversions * 100.0 / Visits).ToString("0.0", CultureInfo.InvariantCulture) + "%";
        }
    }
}
