using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarDisplayYearMinDateModel : BaseModel
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
            Calendar1.Text = DateTime.Now.Year.ToString();

            Calendar1.MinDate = DateTime.Now.AddYears(-5);
            Calendar1.MaxDate = DateTime.Now.AddYears(5);

            Button1.Text = String.Format("选中{0}", DateTime.Now.AddYears(2).Year);
        }


        public static readonly string Calendar1DateFormatString = "yyyy";



        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult(Calendar1.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var text = DateTime.Now.AddYears(2).Year.ToString();
            Calendar1.Text = text;

            UpdateResult(text);
        }


        private void UpdateResult(string text)
        {
            labResult.Text = String.Format("选择的年份：{0}", text);
        }
    }
}