using System;

namespace FineUI.Core.Examples.Pages.Chart.Line
{
    public partial class MetricSwitchModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = ChartInteractionData.DailyMetric("visits");
                ChartMetricTrend.DataSource = rows;
                ChartMetricTrend.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.MetricSummary("visits", rows);
            }
        }

        protected void ChangeMetric(object sender, EventArgs e)
        {
            var metric = rblMetric.SelectedValue;
            var rows = ChartInteractionData.DailyMetric(metric);
            ChartMetricTrend.DataSource = rows;
            ChartMetricTrend.DataBind();
            ToolbarTextChartScope.Text = ChartInteractionData.MetricSummary(metric, rows);
        }
    }
}
