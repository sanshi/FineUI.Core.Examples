using System;
using System.Linq;
using System.Globalization;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class SalesThresholdModel : PageBase
    {
        // 这页单独使用一组差距较大的金额。比如选择 4,500 元时，西北的 3,500 元柱会被筛掉。
        private static readonly ChartRow[] ThresholdSales =
        {
            new ChartRow("east", "华东", 68000),
            new ChartRow("south", "华南", 49000),
            new ChartRow("north", "华北", 32000),
            new ChartRow("southwest", "西南", 19000),
            new ChartRow("central", "华中", 9500),
            new ChartRow("northwest", "西北", 3500),
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSales("0");
            }
        }

        protected void ddlMinimum_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSales(ddlMinimum.SelectedValue);
        }

        private void BindSales(string minimum)
        {
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

            return ThresholdSales.Where(row => row.Amount >= value).ToArray();
        }

        private static string ThresholdSummary(ChartRow[] rows)
        {
            if (rows == null)
            {
                return "请选择有效的最低销售额";
            }
            return rows.Length == 0 ? "没有达到门槛的区域" : $"符合门槛的区域：{rows.Length} 个";
        }
    }
}
