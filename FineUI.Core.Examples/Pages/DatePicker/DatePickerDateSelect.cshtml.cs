using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DatePicker
{
    public partial class DatePickerDateSelectModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void DatePicker1_TextChanged(object sender, EventArgs e)
        {
            if (DatePicker1.SelectedDate.HasValue)
            {
                DatePicker2.SelectedDate = DatePicker1.SelectedDate.Value.AddDays(3);
            }

        }

        protected void DatePicker3_DateSelect(object sender, EventArgs e)
        {
            if (DatePicker3.SelectedDate.HasValue)
            {
                DatePicker4.SelectedDate = DatePicker3.SelectedDate.Value.AddDays(3);
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            labResult1.Text = String.Format("开始日期：{0}  结束日期：{1}", DatePicker1.Text, DatePicker2.Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            labResult2.Text = String.Format("开始日期：{0}  结束日期：{1}", DatePicker3.Text, DatePicker4.Text);
        }

    }
}