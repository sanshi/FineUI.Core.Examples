using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class TimePickerModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var result = String.Format("日期：{0}  时间：{1}",
                DatePicker1.Text,
                TimePicker1.Text);

            labResult.Text = result;
        }

        protected void btnToggleClickAction_Click(object sender, EventArgs e)
        {
            DatePicker1.EnableClickAction = !DatePicker1.EnableClickAction;
            TimePicker1.EnableClickAction = !TimePicker1.EnableClickAction;
            labClickAction.Text = $"日期：{(DatePicker1.EnableClickAction ? "开启" : "关闭")}；时间：{(TimePicker1.EnableClickAction ? "开启" : "关闭")}";
        }

    }
}