using System;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class RegionFilterModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegions(cblRegions.SelectedValueArray);
            }
        }

        private void BindRegions(string[] regionKeys)
        {
            var totals = ChartInteractionData.RegionTrend(regionKeys);
            ChartAreaSales.DataSource = totals;
            ChartAreaSales.DataBind();
            ToolbarTextChartScope.Text = ChartInteractionData.AreaSummary(totals);
        }

        protected void cblRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRegions(cblRegions.SelectedValueArray);
        }
    }
}
