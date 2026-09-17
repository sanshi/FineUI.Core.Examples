using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormValidateValidatorModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("用户名：{0} 密码：{1}", tbxUserName.Text, tbxPassword.Text));
        }

    }
}