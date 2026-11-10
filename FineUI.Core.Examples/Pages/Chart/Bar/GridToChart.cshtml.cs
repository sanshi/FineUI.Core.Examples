using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class GridToChartModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridRegions.DataSource = ChartInteractionData.RegionTotals();
                GridRegions.DataBind();
                BindRegion("east");
            }
        }

        private void BindRegion(string regionKey)
        {
            var region = ChartInteractionData.RegionName(regionKey);
            if (region == null)
            {
                return;
            }

            ChartRegionMonths.DataSource = ChartInteractionData.RegionTrend(regionKey);
            ChartRegionMonths.DataBind();
            ToolbarTextChartScope.Text = region + " · 月度销售额";
        }

        protected void GridRegions_RowClick(object sender, GridRowEventArgs e)
        {
            BindRegion(e.RowID);
        }
    }
}
