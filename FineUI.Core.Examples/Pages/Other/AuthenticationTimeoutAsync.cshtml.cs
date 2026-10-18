using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class AuthenticationTimeoutAsyncModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button2_Click(object sender, EventArgs e)
        {
            // 回发返回跳转命令，异步确认取消时可以阻止本次导航。
            FineUI.Core.PageContext.Redirect(Url.Content("~/?ReturnUrl=%2fOther%2fAuthenticationTimeoutAsync"));
        }

    }
}
