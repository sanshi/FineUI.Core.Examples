using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class TabScriptsModel : LegacyScriptPageBase
    {
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference(
                "legacyTab", Url.Content("~/Other/LegacyScripts/Child"), "旧脚本选项卡", true));
        }
    }
}
