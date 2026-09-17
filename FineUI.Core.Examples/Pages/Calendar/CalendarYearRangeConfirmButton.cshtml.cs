using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarYearRangeConfirmButtonModel : BaseModel
    {
        private int startYear = DateTime.Now.AddYears(2).Year;
        private int endYear = DateTime.Now.AddYears(10).Year;

        public static readonly string Calendar1DateFormatString = "yyyy";

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

            Calendar1.RangeStartDate = DateTime.Now.AddYears(0);
            Calendar1.RangeEndDate = DateTime.Now.AddYears(5);

            Calendar1.MinDate = DateTime.Now.AddYears(-5);
            Calendar1.MaxDate = DateTime.Now.AddYears(15);

            Button1.Text = String.Format("选中范围：{0} - {1}", startYear, endYear);
        }


        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Calendar1.Text = String.Format("{0} - {1}", startYear, endYear);

            UpdateResult();
        }


        private void UpdateResult()
        {
            labResult.Text = String.Format("年份范围：{0}", Calendar1.Text);
        }
    }
}