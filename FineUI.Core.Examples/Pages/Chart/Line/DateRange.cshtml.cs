using System;
using System.Globalization;
using System.Linq;
using ChartRow = FineUI.Core.Examples.Pages.Chart.ChartDataRows.ChartRow;

namespace FineUI.Core.Examples.Pages.Chart.Line
{
    public partial class DateRangeModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpVisitRange.Text = DailyVisitData.FirstDate + " - " + DailyVisitData.LastDate;
                BindVisits(DailyVisitData.FirstDate, DailyVisitData.LastDate);
            }
        }

        private void BindVisits(string start, string end)
        {
            var visits = DailyVisitData.DailyVisits(start, end);
            if (visits == null)
            {
                // 保留上一次有效查询。例如开始日期为 9 月 8 日、结束日期为 9 月 3 日时只更新提示。
                ToolbarTextChartScope.Text = "请选择有效日期，开始日期须早于或等于结束日期";
                return;
            }

            ChartVisitRange.DataSource = visits;
            ChartVisitRange.DataBind();
            ToolbarTextChartScope.Text = VisitSummary(visits);
        }

        protected void dpVisitRange_TextChanged(object sender, EventArgs e)
        {
            BindVisits(
                dpVisitRange.RangeStartDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                dpVisitRange.RangeEndDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }

        private static string VisitSummary(ChartRow[] rows)
        {
            return rows.Length == 0 ? "所选日期内没有访问数据" : $"{rows.Length} 天 · 访问量 {rows.Sum(row => row.Amount)}";
        }
    }
}
