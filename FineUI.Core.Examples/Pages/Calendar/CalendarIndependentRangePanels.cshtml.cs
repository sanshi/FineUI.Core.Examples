using System;
using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarIndependentRangePanelsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Calendar1.RangeStartDate = DateTime.Parse("2020-03-02");
                Calendar1.RangeEndDate = DateTime.Parse("2026-08-18");

                Calendar2.RangeStartDate = DateTime.Parse("2020-03-01");
                Calendar2.RangeEndDate = DateTime.Parse("2026-08-01");

                Calendar3.RangeStartDate = DateTime.Parse("2001-01-01");
                Calendar3.RangeEndDate = DateTime.Parse("2026-01-01");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("日期范围：{0}<br>月份范围：{1}<br>年份范围：{2}",
                Calendar1.Text, Calendar2.Text, Calendar3.Text);
        }
    }
}
