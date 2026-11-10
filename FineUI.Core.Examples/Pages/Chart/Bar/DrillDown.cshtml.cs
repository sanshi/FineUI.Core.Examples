using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class DrillDownModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegionTotals();
            }
        }

        private void BindRegionTotals()
        {
            ChartRegionSales.DataSource = RegionSalesData.RegionTotals();
            ChartRegionSales.DataBind();
            ToolbarTextChartScope.Text = "各区域销售额";
            btnBack.Enabled = false;
        }

        protected void ChartRegionSales_PointClick(object sender, ChartPointEventArgs e)
        {
            // 只有区域可继续钻取。上海等城市的键没有下一级数据，保留当前城市明细。
            var cities = RegionSalesData.CityTotals(e.DataKey);
            if (cities.Length == 0)
            {
                return;
            }

            ChartRegionSales.DataSource = cities;
            ChartRegionSales.DataBind();
            ToolbarTextChartScope.Text = RegionSalesData.RegionName(e.DataKey) + " · 城市销售额";
            btnBack.Enabled = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            BindRegionTotals();
        }
    }
}
