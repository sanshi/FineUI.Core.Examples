using System;

namespace FineUI.Core.Examples.Pages.Chart.Line
{
    public partial class DateFocusModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dpFocus.Text = "2026-09-01";
                var rows = ChartInteractionData.DailyFocus(ChartInteractionData.FirstDate);
                ChartDateFocus.DataSource = rows;
                ChartDateFocus.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.VisitFocusSummary(ChartInteractionData.FirstDate);
            }
        }

        protected void FocusDate(object sender, ChartPointEventArgs e)
        {
            var seriesKey = e.SeriesKey;
            var dataKey = e.DataKey;
            if (seriesKey == "amount" && ChartInteractionData.FindVisit(dataKey) != null)
            {
                dpFocus.Text = dataKey;
                ChartDateFocus.DataSource = ChartInteractionData.DailyFocus(dataKey);
                ChartDateFocus.DataBind();
                ToolbarTextChartScope.Text = ChartInteractionData.VisitFocusSummary(dataKey);
            }
        }

        protected void ReadDate(object sender, EventArgs e)
        {
            var date = dpFocus.Text;
            ChartDateFocus.DataSource = ChartInteractionData.DailyFocus(date);
            ChartDateFocus.DataBind();
            ToolbarTextChartScope.Text = ChartInteractionData.VisitFocusSummary(date);
        }
    }
}
