using System;
using System.Linq;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class CumulativeModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = AreaAmounts(false);
                ChartCumulativeSales.DataSource = rows;
                ChartCumulativeSales.DataBind();
                ToolbarTextChartScope.Text = AreaModeSummary(false);
            }
        }

        protected void cbxCumulative_CheckedChanged(object sender, EventArgs e)
        {
            var cumulative = cbxCumulative.Checked;
            var rows = AreaAmounts(cumulative);
            ChartCumulativeSales.DataSource = rows;
            ChartCumulativeSales.DataBind();
            ToolbarTextChartScope.Text = AreaModeSummary(cumulative);
        }

        private static ChartRow[] AreaAmounts(bool cumulative)
        {
            var rows = RegionSalesData.RegionTrend();
            if (!cumulative)
            {
                return rows;
            }

            // 截至当月包含之前各月。比如 5 月的累计值是 4 月和 5 月的销售额之和。
            int total = 0;
            return rows.Select(row =>
            {
                total += row.Amount;
                return new ChartRow(row.Key, row.Category, total);
            }).ToArray();
        }

        private static string AreaModeSummary(bool cumulative)
        {
            return cumulative ? "统计口径：截至当月累计" : "统计口径：本月金额";
        }
    }
}
