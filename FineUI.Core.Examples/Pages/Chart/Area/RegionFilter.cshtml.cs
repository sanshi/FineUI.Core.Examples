using System;
using System.Linq;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

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
            var totals = RegionSalesData.RegionTrend(regionKeys);
            ChartAreaSales.DataSource = totals;
            ChartAreaSales.DataBind();
            ToolbarTextChartScope.Text = AreaSummary(totals);
        }

        protected void cblRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRegions(cblRegions.SelectedValueArray);
        }

        private static string AreaSummary(ChartRow[] rows)
        {
            return rows.Length == 0 ? "请选择至少一个区域" : $"所选区域销售额合计 {rows.Sum(row => row.Amount)} 元";
        }
    }
}
