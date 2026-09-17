using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerYearRangeConfirmButtonModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DatePicker1.RangeStartDate = DateTime.Now.AddYears(0);
                DatePicker1.RangeEndDate = DateTime.Now.AddYears(5);

                DatePicker1.MinDate = DateTime.Now.AddYears(-5);
                DatePicker1.MaxDate = DateTime.Now.AddYears(15);
            }
        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = String.Format("年份范围：{0}", DatePicker1.Text);
        }

    }
}