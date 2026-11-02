using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ActiveWindowModel : LegacyScriptPageBase
    {
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Window1.GetShowReference(
                Url.Content("~/Other/LegacyScripts/ActiveWindowChild"), "旧保存关窗写法"));
        }
    }
}
