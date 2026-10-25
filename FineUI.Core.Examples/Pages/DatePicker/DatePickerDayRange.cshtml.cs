using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerDayRangeModel : PageBase
    {
        protected void btnMoveStart_Click(object sender, EventArgs e)
        {
            DateTime? start = DatePicker1.RangeStartDate;
            DateTime? end = DatePicker1.RangeEndDate;
            if (start.HasValue && end.HasValue && start.Value < end.Value)
            {
                // 只改开始日期，保留用户当前选择的结束日期。
                DatePicker1.RangeStartDate = start.Value.AddDays(1);
            }
            labResult.Text = String.Format("选择的日期：{0}", DatePicker1.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DatePicker1.RangeStartDate = DateTime.Parse("2014-07-30");
                DatePicker1.RangeEndDate = DateTime.Parse("2014-08-08");
            }
        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("选择的日期：{0}", DatePicker1.Text);
        }

    }
}