using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarDisplayMonthModel : BaseModel
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
            Calendar1.Text = DateTime.Now.ToString(Calendar1DateFormatString);
            Button1.Text = String.Format("选中{0}", DateTime.Now.AddMonths(2).ToString(Calendar1DateFormatString));
        }

        public static readonly string Calendar1DateFormatString = "yyyy/MM";



        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult(Calendar1.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var text = DateTime.Now.AddMonths(2).ToString(Calendar1DateFormatString);
            Calendar1.Text = text;

            UpdateResult(text);
        }


        private void UpdateResult(string text)
        {
            labResult.Text = String.Format("选择的年月：{0}", text);
        }
    }
}