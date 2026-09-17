using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarDayRangeTimeShowSecondModel : BaseModel
    {
        private DateTime GetStartDate()
        {
            var date = DateTime.Now.AddDays(2);
            return new DateTime(date.Year, date.Month, date.Day, 8, 50, 0);
        }
        private DateTime GetEndDate()
        {
            var date = DateTime.Now.AddDays(20);
            return new DateTime(date.Year, date.Month, date.Day, 11, 50, 0);
        }

        public static readonly string Calendar1DateFormatString = "yyyy/MM/dd HH:mm";

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

            Calendar1.RangeStartDate = DateTime.Parse("2014-07-30 14:30:00");
            Calendar1.RangeEndDate = DateTime.Parse("2014-08-08 16:30:00");

            Button1.Text = String.Format("选中范围：{0} - {1}", GetStartDate().ToString(Calendar1.DateFormatString), GetEndDate().ToString(Calendar1.DateFormatString));
        }

        
        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Calendar1.RangeStartDate = GetStartDate();
            Calendar1.RangeEndDate = GetEndDate();

            UpdateResult();
        }


        private void UpdateResult()
        {
            labResult.Text = String.Format("选择的日期：{0}", Calendar1.Text);
        }
    }
}