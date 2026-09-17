using System;
using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerIndependentRangePanelsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DatePicker1.RangeStartDate = DateTime.Parse("2020-03-02");
                DatePicker1.RangeEndDate = DateTime.Parse("2026-08-18");

                DatePicker2.RangeStartDate = DateTime.Parse("2020-03-01");
                DatePicker2.RangeEndDate = DateTime.Parse("2026-08-01");

                DatePicker3.RangeStartDate = DateTime.Parse("2001-01-01");
                DatePicker3.RangeEndDate = DateTime.Parse("2026-01-01");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("日期范围：{0}<br>月份范围：{1}<br>年份范围：{2}",
                DatePicker1.Text, DatePicker2.Text, DatePicker3.Text);
        }
    }
}
