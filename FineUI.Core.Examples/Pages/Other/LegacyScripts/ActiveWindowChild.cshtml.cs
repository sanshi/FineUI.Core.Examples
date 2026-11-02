using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ActiveWindowChildModel : LegacyScriptPageBase
    {
        protected void btnSave_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(ActiveWindow.GetHideReference()
                + "parent.F.ui.labResult.setText('子窗口已保存并关闭');");
        }

        protected void btnCallback_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(ActiveWindow.GetHideExecuteScriptReference(
                "F.ui.labResult.setText('关闭后的父页脚本已执行');"));
        }
    }
}
