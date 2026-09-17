using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarMonthRangeConfirmButtonModel : BaseModel
    {
        private DateTime startDate = DateTime.Now.AddMonths(2);
        private DateTime endDate = DateTime.Now.AddMonths(10);

        public static readonly string Calendar1DateFormatString = "yyyy-MM";

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

            Calendar1.RangeStartDate = DateTime.Now.AddMonths(0);
            Calendar1.RangeEndDate = DateTime.Now.AddMonths(5);

            Calendar1.MinDate = DateTime.Now.AddMonths(-5);
            Calendar1.MaxDate = DateTime.Now.AddMonths(15);

            Button1.Text = String.Format("选中范围：{0} - {1}", startDate.ToString(Calendar1.DateFormatString), endDate.ToString(Calendar1.DateFormatString));
        }


        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Calendar1.RangeStartDate = startDate;
            Calendar1.RangeEndDate = endDate;

            UpdateResult();
        }


        private void UpdateResult()
        {
            labResult.Text = String.Format("月份范围：{0}", Calendar1.Text);
        }
    }
}