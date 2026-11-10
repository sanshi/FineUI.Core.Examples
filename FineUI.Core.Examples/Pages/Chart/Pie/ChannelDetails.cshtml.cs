using System;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class ChannelDetailsModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = ChartInteractionData.ChannelTotals();
                ChartChannelDetails.DataSource = rows;
                ChartChannelDetails.DataBind();
                GridChannelMonths.DataSource = new ChartInteractionData.ChartRow[0];
                GridChannelMonths.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.ChannelDetailsSummary(null, null);
            }
        }

        protected void ChartChannelDetails_PointClick(object sender, ChartPointEventArgs e)
        {
            var seriesKey = e.SeriesKey;
            var dataKey = e.DataKey;
            if (seriesKey == "amount" && ChartInteractionData.ChannelName(dataKey) != null)
            {
                var rows = ChartInteractionData.ChannelTrend(dataKey);
                GridChannelMonths.DataSource = rows;
                GridChannelMonths.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.ChannelDetailsSummary(dataKey, rows);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            var rows = new ChartInteractionData.ChartRow[0];
            GridChannelMonths.DataSource = rows;
            GridChannelMonths.DataBind();
            ToolbarTextChartScope.Text = ChartInteractionData.ChannelDetailsSummary(null, rows);
        }
    }
}
