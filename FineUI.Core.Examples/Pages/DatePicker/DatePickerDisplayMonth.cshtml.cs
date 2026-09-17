using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerDisplayMonthModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var result = String.Format("开始年月：{0}  结束年月：{1}",
                DatePicker1.Text,
                DatePicker2.Text);

            labResult.Text = result;
        }

    }
}