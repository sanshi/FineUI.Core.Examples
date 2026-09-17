using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class NumberBoxDecimalPrecisionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify("数字输入框的值：" + NumberBox1.Text);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            NumberBox1.DecimalPrecision = 1;
            NumberBox1.Increment = 0.1;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            NumberBox1.DecimalPrecision = 2;
            NumberBox1.Increment = 0.01;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            NumberBox1.DecimalPrecision = 3;
            NumberBox1.Increment = 0.001;
        }

    }
}