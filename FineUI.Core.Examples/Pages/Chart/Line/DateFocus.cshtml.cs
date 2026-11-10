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
                dpFocus.Text = "2026-09-01";
                var rows = DailyFocus(DailyVisitData.FirstDate);
                ChartDateFocus.DataSource = rows;
                ChartDateFocus.DataBind();
                ToolbarTextChartScope.Text = VisitFocusSummary(DailyVisitData.FirstDate);
            }
        }

        protected void ChartDateFocus_PointClick(object sender, ChartPointEventArgs e)
        {
            var seriesKey = e.SeriesKey;
            var pointId = e.PointID;
            if (seriesKey == "amount" && DailyVisitData.FindVisit(pointId) != null)
            {
                dpFocus.Text = pointId;
                ChartDateFocus.DataSource = DailyFocus(pointId);
                ChartDateFocus.DataBind();
                ToolbarTextChartScope.Text = VisitFocusSummary(pointId);
            }
        }

        protected void btnFocus_Click(object sender, EventArgs e)
        {
            var date = dpFocus.Text;
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
