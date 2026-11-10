using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class SalesThresholdModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nbxMinimum.Text = "0";
                var rows = ChartInteractionData.RegionThreshold("0");
                ChartSalesThreshold.DataSource = rows;
                ChartSalesThreshold.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.ThresholdSummary(rows);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var minimum = nbxMinimum.Text;
            var rows = ChartInteractionData.RegionThreshold(minimum);
            if (rows != null)
            {
                ChartSalesThreshold.DataSource = rows;
                ChartSalesThreshold.DataBind();
            }
            ToolbarTextChartScope.Text = ChartInteractionData.ThresholdSummary(rows);
        }
    }
}
