using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class FluentLoginModel : PageBase
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(string userName, string password)
        {
            string message = userName == "admin" && password == "admin888"
                ? "登录成功：admin" : "用户名或密码错误";
            UIHelper.Label("labResult").Text(message);
            return UIHelper.Result();
        }
    }
}
