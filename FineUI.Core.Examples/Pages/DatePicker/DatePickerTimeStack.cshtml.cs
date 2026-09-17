using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerTimeStackModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var result = String.Format("开始日期：{0}  结束日期：{1}",
                DatePicker1.Text,
                DatePicker2.Text);

            labResult.Text = result;
        }

    }
}
