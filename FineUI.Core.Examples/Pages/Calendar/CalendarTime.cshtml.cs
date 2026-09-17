using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarTimeModel : BaseModel
    {
        public static readonly string Calendar1DateFormatString = "yyyy/MM/dd HH:mm:ss";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            Calendar1.DateFormatString = Calendar1DateFormatString;
            Calendar1.SelectedDate = DateTime.Now.AddDays(10);

            DateTime newDate = DateTime.Now.AddDays(2);
            newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, 0, 0, 0);
            Button1.Text = String.Format("选中{0}", newDate.ToString(Calendar1DateFormatString));
        }


        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult(Calendar1.SelectedDate.Value);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var newDate = DateTime.Now.AddDays(2);
            newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, 0, 0, 0);
            Calendar1.SelectedDate = newDate;

            UpdateResult(newDate);
        }


        private void UpdateResult(DateTime date)
        {
            labResult.Text = String.Format("选择的日期：{0}", date.ToString(Calendar1DateFormatString));
        }
    }
}
