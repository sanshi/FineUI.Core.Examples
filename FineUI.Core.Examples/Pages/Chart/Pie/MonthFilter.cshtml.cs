using System;
using System.Linq;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class MonthFilterModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = ChannelVisitData.ChannelTotals();
                ChartChannelMonth.DataSource = rows;
                ChartChannelMonth.DataBind();
                ToolbarTextChartScope.Text = DistributionSummary("all", rows);
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var month = ddlMonth.SelectedValue;
            var rows = month == "all" ? ChannelVisitData.ChannelTotals() : ChannelVisitData.ChannelTotals(month);
            if (rows.Length > 0)
            {
                ChartChannelMonth.DataSource = rows;
                ChartChannelMonth.DataBind();
                ToolbarTextChartScope.Text = DistributionSummary(month, rows);
            }
        }

        private static string DistributionSummary(string month, ChartRow[] rows)
        {
            var range = month == "all" ? "全部月份" : Array.Find(ChannelVisitData.ChannelTrend(), row => row.Id == month)?.Category;
            return $"{range} · 总访问量 {rows.Sum(row => row.Amount)}";
        }
    }
}
