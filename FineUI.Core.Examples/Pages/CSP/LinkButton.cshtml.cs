using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.CSP
{
    public partial class LinkButtonModel : BaseModel
    {
        public void OnGet()
        {
            var pm = PageManager.Instance;
            pm.CspScripts = true;
            pm.CspScriptsAllowNonce = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lbServer_Click(object sender, EventArgs e)
        {
            ShowNotify("你点击了链接按钮（服务器端事件）");
        }

        protected void lbConfirm_Click(object sender, EventArgs e)
        {
            ShowNotify("确认通过，已回发");
        }
    }
}
