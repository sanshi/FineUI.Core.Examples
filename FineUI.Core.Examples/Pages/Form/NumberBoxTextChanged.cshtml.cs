using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class NumberBoxTextChangedModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void NumberBox3_TextChanged(object sender, EventArgs e)
        {
            ShowNotify("数字输入框的值（NumberBox3_TextChanged）：" + NumberBox3.Text);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify("数字输入框的值（btnSubmit_Click）：" + NumberBox3.Text);
        }


    }
}