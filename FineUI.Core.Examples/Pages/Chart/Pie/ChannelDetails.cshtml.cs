using System;
using System.Linq;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class ChannelDetailsModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = ChannelVisitData.ChannelTotals();
                ChartChannelDetails.DataSource = rows;
                ChartChannelDetails.DataBind();
                GridChannelMonths.DataSource = new ChartRow[0];
                GridChannelMonths.DataBind();
                ToolbarTextChartScope.Text = ChannelDetailsSummary(null, null);
            }
        }

        protected void ChartChannelDetails_PointClick(object sender, ChartPointEventArgs e)
        {
            var seriesKey = e.SeriesKey;
            var dataKey = e.DataKey;
            if (seriesKey == "amount" && ChannelVisitData.ChannelName(dataKey) != null)
            {
                var rows = ChannelVisitData.ChannelTrend(dataKey);
                GridChannelMonths.DataSource = rows;
                GridChannelMonths.DataBind();
                ToolbarTextChartScope.Text = ChannelDetailsSummary(dataKey, rows);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            var rows = new ChartRow[0];
            GridChannelMonths.DataSource = rows;
            GridChannelMonths.DataBind();
            ToolbarTextChartScope.Text = ChannelDetailsSummary(null, rows);
        }

        private static string ChannelDetailsSummary(string key, ChartRow[] rows)
        {
            return key == null ? "请选择一个渠道" : $"{ChannelVisitData.ChannelName(key)} · 六个月访问量 {rows.Sum(row => row.Amount)}";
        }
    }
}
