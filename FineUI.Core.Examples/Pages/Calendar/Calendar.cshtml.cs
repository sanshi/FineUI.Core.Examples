using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarModel : BaseModel
    {
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
            Button1.Text = String.Format("选中{0}", DateTime.Now.AddDays(2).ToString(Calendar1DateFormatString));
        }

        public static readonly string Calendar1DateFormatString = "yyyy/MM/dd";

        
        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult(Calendar1.SelectedDate.Value);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var selectedDate = DateTime.Now.AddDays(2);
            Calendar1.SelectedDate = selectedDate;

            UpdateResult(selectedDate);
        }


        private void UpdateResult(DateTime date)
        {
            labResult.Text = String.Format("选择的日期：{0}", date.ToString(Calendar1DateFormatString));
        }
    }
}