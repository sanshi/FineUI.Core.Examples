using System;
using System.Linq;
using System.Globalization;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class SalesThresholdModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                nbxMinimum.Text = "0";
                var rows = RegionThreshold("0");
                ChartSalesThreshold.DataSource = rows;
                ChartSalesThreshold.DataBind();
                ToolbarTextChartScope.Text = ThresholdSummary(rows);
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var minimum = nbxMinimum.Text;
            var rows = RegionThreshold(minimum);
            if (rows != null)
            {
                ChartSalesThreshold.DataSource = rows;
                ChartSalesThreshold.DataBind();
            }
            ToolbarTextChartScope.Text = ThresholdSummary(rows);
        }

        private static ChartRow[] RegionThreshold(string minimum)
        {
            if (!int.TryParse(minimum, NumberStyles.None, CultureInfo.InvariantCulture, out int value))
            {
                return null;
            }

            return RegionSalesData.RegionTotals().Where(row => row.Amount >= value).OrderByDescending(row => row.Amount).ToArray();
        }

        private static string ThresholdSummary(ChartRow[] rows)
        {
            if (rows == null)
            {
                return "请输入非负整数金额";
            }
            return rows.Length == 0 ? "没有达到门槛的区域" : $"符合门槛的区域：{rows.Length} 个";
        }
    }
}
