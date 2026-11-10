using System.Collections.Generic;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public static class SalesOrderData
    {
        private static readonly SaleOrder[] Orders =
        {
            new SaleOrder { Month = "2026-04", Number = "SO-401", Customer = "华东门店", Amount = 2650 },
            new SaleOrder { Month = "2026-04", Number = "SO-402", Customer = "北区经销", Amount = 3380 },
            new SaleOrder { Month = "2026-04", Number = "SO-403", Customer = "海滨商贸", Amount = 2200 },
            new SaleOrder { Month = "2026-05", Number = "SO-501", Customer = "南区直营", Amount = 4920 },
            new SaleOrder { Month = "2026-05", Number = "SO-502", Customer = "华东门店", Amount = 3650 },
            new SaleOrder { Month = "2026-05", Number = "SO-503", Customer = "北区经销", Amount = 3850 },
            new SaleOrder { Month = "2026-06", Number = "SO-601", Customer = "西区门店", Amount = 2860 },
            new SaleOrder { Month = "2026-06", Number = "SO-602", Customer = "海滨商贸", Amount = 4140 },
            new SaleOrder { Month = "2026-06", Number = "SO-603", Customer = "南区直营", Amount = 3180 },
            new SaleOrder { Month = "2026-07", Number = "SO-701", Customer = "华东门店", Amount = 5800 },
            new SaleOrder { Month = "2026-07", Number = "SO-702", Customer = "西区门店", Amount = 3620 },
            new SaleOrder { Month = "2026-07", Number = "SO-703", Customer = "北区经销", Amount = 4610 },
            new SaleOrder { Month = "2026-08", Number = "SO-801", Customer = "海滨商贸", Amount = 4500 },
            new SaleOrder { Month = "2026-08", Number = "SO-802", Customer = "北区经销", Amount = 3480 },
            new SaleOrder { Month = "2026-08", Number = "SO-803", Customer = "南区直营", Amount = 3730 },
            new SaleOrder { Month = "2026-09", Number = "SO-901", Customer = "华东门店", Amount = 6850 },
            new SaleOrder { Month = "2026-09", Number = "SO-902", Customer = "西区门店", Amount = 4050 },
            new SaleOrder { Month = "2026-09", Number = "SO-903", Customer = "南区直营", Amount = 4920 }
        };

        private static readonly string[] RecentMonths = Orders.Select(row => row.Month)
            .Distinct().OrderByDescending(month => month).Take(2).ToArray();

        public static IEnumerable<object> Months(bool recent = false)
        {
            return OrdersForRange(recent)
                .GroupBy(row => row.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    Category = group.Key.Substring(5).TrimStart('0') + " 月",
                    Amount = group.Sum(row => row.Amount)
                }).ToArray();
        }

        public static SaleOrder[] Details(string month = null, bool recent = false)
        {
            return OrdersForRange(recent).Where(row => string.IsNullOrEmpty(month) || row.Month == month).ToArray();
        }

        private static IEnumerable<SaleOrder> OrdersForRange(bool recent)
        {
            return recent ? Orders.Where(row => RecentMonths.Contains(row.Month)) : Orders;
        }

        public static string OrderScope(string month, bool recent, int count)
        {
            string range = recent ? "最近两个月" : "全部月份";
            if (!string.IsNullOrEmpty(month))
            {
                range = month;
            }
            return $"{range} · {count} 笔订单";
        }

        public static bool HasMonth(string month)
        {
            return Orders.Any(row => row.Month == month);
        }

        public sealed class SaleOrder
        {
            public string Month { get; set; }
            public string Number { get; set; }
            public string Customer { get; set; }
            public int Amount { get; set; }
        }
    }
}
