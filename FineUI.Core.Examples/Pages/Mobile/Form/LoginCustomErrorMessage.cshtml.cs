using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Mobile.Form
{
    public partial class LoginCustomErrorMessageModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxPassword.Text == "admin")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify("用户名或密码错误！", MessageBoxIcon.Error);
            }
        }

    }
}