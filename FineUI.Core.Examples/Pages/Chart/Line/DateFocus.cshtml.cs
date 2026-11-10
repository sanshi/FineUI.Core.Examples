using System;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Chart.Line
{
    public partial class DateFocusModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpFocus.Text = DailyVisitData.FirstDate;
                BindDateFocus(DailyVisitData.FirstDate);
            }
        }

        protected void ChartDateFocus_PointClick(object sender, ChartPointEventArgs e)
        {
            var pointId = e.PointID;
            if (DailyVisitData.FindVisit(pointId) != null)
            {
                dpFocus.Text = pointId;
                BindDateFocus(pointId);
            }
        }

        protected void dpFocus_TextChanged(object sender, EventArgs e)
        {
            BindDateFocus(dpFocus.Text);
        }

        private void BindDateFocus(string date)
        {
            ChartDateFocus.DataSource = DailyFocus(date);
            ChartDateFocus.DataBind();
            ToolbarTextChartScope.Text = VisitFocusSummary(date);
        }

        private static object DailyFocus(string key)
        {
            return DailyVisitData.DailyVisits().Select(row => new
            {
                row.Id,
                row.Category,
                row.Amount,
                // 只在选中的日期显示标记和数值，其余日期的访问趋势由主折线保留。
                FocusAmount = row.Id == key ? (int?)row.Amount : null,
                Color = row.Id == key ? "#d9480f" : ""
            }).ToArray();
        }

        private static string VisitFocusSummary(string key)
        {
            var details = DailyVisitData.FindVisit(key);
            return details == null ? "所选日期没有访问数据" : $"{key} · 访问量 {details.Visits} · 转化量 {details.Conversions}";
        }
    }
}
