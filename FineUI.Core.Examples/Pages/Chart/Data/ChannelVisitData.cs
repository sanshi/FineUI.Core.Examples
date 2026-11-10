using System;
using System.Linq;
using static FineUI.Core.Examples.Pages.Chart.ChartDataRows;

namespace FineUI.Core.Examples.Pages.Chart
{
    internal static class ChannelVisitData
    {
        private static readonly MonthlyValues[] Channels =
        {
            new MonthlyValues("search", "搜索", 2900, 3200, 3140, 3470, 3890, 4120),
            new MonthlyValues("direct", "直接访问", 1280, 1400, 1530, 1550, 1640, 1890),
            new MonthlyValues("referral", "外部链接", 920, 1010, 970, 1100, 1260, 1210),
            new MonthlyValues("campaign", "推广活动", 640, 950, 710, 1230, 1380, 1190),
        };

        internal static ChartRow[] ChannelTotals() => Totals(Channels);

        internal static string ChannelName(string key) => Name(Channels, key);

        internal static ChartRow[] ChannelTrend(string key = null)
        {
            return Trend(Channels.Where(channel => key == null || channel.Key == key).ToArray());
        }

        internal static ChartRow[] ChannelTotals(string month)
        {
            int index = Array.FindIndex(ChannelTrend(), row => row.Key == month);
            return index < 0 ? new ChartRow[0]
                : Channels.Select(channel => new ChartRow(channel.Key, channel.Name, channel.Values[index])).ToArray();
        }

    }
}
