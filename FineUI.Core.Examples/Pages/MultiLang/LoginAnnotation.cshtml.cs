using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FineUI.Core.Examples.Pages.MultiLang.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

namespace FineUI.Core.Examples.Pages.MultiLang
{

    public partial class LoginAnnotationModel : BaseMultilangModel
    {

        public void OnGet()
        {
            //CurrentUser = new User { 
            //    UserName = "<b>BBB</b>",
            //    Password = "haha"
            //};
        }

        [BindProperty]
        public User CurrentUser { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            //ModelState.Remove("CurrentUser.Password");

            if (ModelState.IsValid)
            {
                if (CurrentUser.UserName == "admin" && CurrentUser.Password == "admin888")
                {
                    ShowNotify(_R("成功登录！"), MessageBoxIcon.Success);
                }
                else
                {
                    ShowNotify(_R("用户名（{0}）或密码（{1}）错误！", CurrentUser.UserName, CurrentUser.Password), MessageBoxIcon.Error);
                }
            }
        }

    }
}