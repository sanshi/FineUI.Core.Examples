using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TextBoxTextChangedModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            labResult1.Text = "文本框一：" + TextBox1.Text;
        }

        protected void TextBox2_Blur(object sender, EventArgs e)
        {
            labResult2.Text = "文本框二：" + TextBox2.Text;
        }

    }
}