using System;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class MonthFilterModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = ChartInteractionData.ChannelDistribution("all");
                ChartChannelMonth.DataSource = rows;
                ChartChannelMonth.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.DistributionSummary("all", rows);
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var month = ddlMonth.SelectedValue;
            var rows = ChartInteractionData.ChannelDistribution(month);
            if (rows.Length > 0)
            {
                ChartChannelMonth.DataSource = rows;
                ChartChannelMonth.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.DistributionSummary(month, rows);
            }
        }
    }
}
