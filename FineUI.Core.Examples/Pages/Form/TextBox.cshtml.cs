using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TextBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Text = "用户名：" + tbxUserName.Text + " 密码：" + tbxPassword.Text;
        }

    }
}