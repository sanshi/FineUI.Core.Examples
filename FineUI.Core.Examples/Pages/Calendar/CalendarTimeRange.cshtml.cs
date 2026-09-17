using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Calendar
{
    public partial class CalendarTimeRangeModel : BaseModel
    {
        public static readonly string Calendar1DateFormatString = "HH:mm:ss";

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

            Calendar1.Text = "14:30:00 - 16:30:00";

            Button1.Text = String.Format("选中范围：{0} - {1}", "08:50:00", "11:50:00");
        }

        
        protected void Calendar1_DateSelect(object sender, EventArgs e)
        {
            UpdateResult();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Calendar1.Text = "08:50:00 - 11:50:00";

            UpdateResult();
        }


        private void UpdateResult()
        {
            labResult.Text = String.Format("时间范围：{0}", Calendar1.Text);
        }
    }
}