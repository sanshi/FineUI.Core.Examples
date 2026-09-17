using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerMinDateModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("选择的日期：{0}", DatePicker1.Text);
        }

    }
}