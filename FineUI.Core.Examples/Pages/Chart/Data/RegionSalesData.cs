using System.Collections.Generic;
using System.Linq;
using static FineUI.Core.Examples.Pages.Chart.ChartDataRows;

namespace FineUI.Core.Examples.Pages.Chart
{
    internal static class RegionSalesData
    {
        // 区域月度销售额，依次对应 2026 年 4 月至 9 月，单位为元。
        private static readonly MonthlyValues[] Regions =
        {
            new MonthlyValues("east", "华东", 6800, 7400, 7100, 8900, 9200, 10100),
            new MonthlyValues("north", "华北", 5400, 6200, 5800, 7100, 7700, 8200),
            new MonthlyValues("south", "华南", 5900, 6300, 6900, 7200, 7900, 8800),
            new MonthlyValues("central", "华中", 5800, 6400, 6700, 7200, 7900, 8800),
            new MonthlyValues("southwest", "西南", 6100, 6800, 7200, 7800, 8400, 9700),
            new MonthlyValues("northwest", "西北", 4100, 4500, 4800, 5400, 5900, 6800),
        };

        // 城市明细按区域键查询。比如 southwest 对应成都、重庆和昆明，三座城市的金额合计为该区域销售额。
        private static readonly Dictionary<string, ChartRow[]> Cities = new Dictionary<string, ChartRow[]>
        {
            ["east"] = new[]
            {
                new ChartRow("shanghai", "上海", 21800),
                new ChartRow("suzhou", "苏州", 14600),
                new ChartRow("hangzhou", "杭州", 13100),
            },
            ["north"] = new[]
            {
                new ChartRow("beijing", "北京", 17100),
                new ChartRow("tianjin", "天津", 12200),
                new ChartRow("jinan", "济南", 11100),
            },
            ["south"] = new[]
            {
                new ChartRow("guangzhou", "广州", 18200),
                new ChartRow("shenzhen", "深圳", 14900),
                new ChartRow("foshan", "佛山", 9900),
            },
            ["central"] = new[]
            {
                new ChartRow("wuhan", "武汉", 18200),
                new ChartRow("changsha", "长沙", 13400),
                new ChartRow("zhengzhou", "郑州", 11200),
            },
            ["southwest"] = new[]
            {
                new ChartRow("chengdu", "成都", 19500),
                new ChartRow("chongqing", "重庆", 16100),
                new ChartRow("kunming", "昆明", 10400),
            },
            ["northwest"] = new[]
            {
                new ChartRow("xian", "西安", 14200),
                new ChartRow("lanzhou", "兰州", 9800),
                new ChartRow("xining", "西宁", 7500),
            },
        };

        internal static ChartRow[] RegionTotals() => Totals(Regions);

        internal static string RegionName(string key) => Name(Regions, key);

        internal static ChartRow[] RegionTrend()
        {
            return Trend(Regions);
        }

        internal static ChartRow[] RegionTrend(string key)
        {
            return Trend(Regions.Where(region => region.Id == key).ToArray());
        }

        internal static ChartRow[] RegionTrend(string[] keys)
        {
            return Trend(Regions.Where(region => keys != null && keys.Contains(region.Id)).ToArray());
        }

        internal static ChartRow[] CityTotals(string regionKey)
        {
            return regionKey != null && Cities.TryGetValue(regionKey, out var rows) ? rows : new ChartRow[0];
        }

    }
}
