using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class LoginModel : BaseModel
    {
        public void OnGet()
        {

        }
        
        public void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = tbxUserName.Text.Trim();
            var password = tbxPassword.Text.Trim();

            if (userName == "admin" && password == "admin888")
            {
                ShowNotify("成功登录！", MessageBoxIcon.Success);
            }
            else
            {
                ShowNotify(String.Format("用户名（{0}）或密码（{1}）错误！",
                        userName,
                        password), MessageBoxIcon.Error);
            }
        }

    }
}