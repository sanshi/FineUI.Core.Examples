using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerDayRangeMinDateModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DatePicker1.MinDate = DateTime.Now;
                DatePicker1.MaxDate = DateTime.Now.AddDays(30);

                DatePicker1.RangeStartDate = DateTime.Now.AddDays(5);
                DatePicker1.RangeEndDate = DateTime.Now.AddDays(10);

            }
        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("选择的日期：{0}", DatePicker1.Text);
        }

    }
}