using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarDisplayTimeModel : BaseModel
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
            Button1.Text = String.Format("选中{0}", "00:00:00");
        }

        public static readonly string Calendar1DateFormatString = "HH:mm:ss";

        

        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult(Calendar1.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var text = "00:00:00";
            Calendar1.Text = text;

            UpdateResult(text);
        }


        private void UpdateResult(string text)
        {
            labResult.Text = String.Format("选择的时间：{0}", text);
        }
    }
}