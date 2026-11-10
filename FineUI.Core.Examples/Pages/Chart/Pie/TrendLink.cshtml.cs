using System;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class TrendLinkModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartChannels.DataSource = ChannelVisitData.ChannelTotals();
                ChartChannels.DataBind();
                BindTrend(null);
            }
        }

        private void BindTrend(string channelKey)
        {
            var channel = channelKey == null ? "全部渠道" : ChannelVisitData.ChannelName(channelKey);
            if (channel == null)
            {
                return;
            }

            ChartChannelTrend.DataSource = ChannelVisitData.ChannelTrend(channelKey);
            ChartChannelTrend.DataBind();
            ToolbarTextChartScope.Text = channel + " · 月度访问趋势";
        }

        protected void ChartChannels_PointClick(object sender, ChartPointEventArgs e)
        {
            BindTrend(e.DataKey);
        }

        protected void btnAllChannels_Click(object sender, EventArgs e)
        {
            BindTrend(null);
        }
    }
}
