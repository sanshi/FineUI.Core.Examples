using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class WindowScriptsModel : LegacyScriptPageBase
    {
        protected void btnShow_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Window1.GetShowReference());
        }

        protected void btnHide_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Window1.GetHideReference());
        }

        protected void btnMaximize_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Window1.GetMaximizeReference());
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Window1.GetRestoreReference());
        }
    }
}
