using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ClientClickModel : LegacyScriptPageBase
    {
        protected void btnServer_Click(object sender, EventArgs e)
        {
            labResult.Text = "后台点击事件已执行";
        }
    }
}
