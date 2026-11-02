using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class MessageCallbacksModel : LegacyScriptPageBase
    {
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Confirm.GetShowReference(
                "是否继续处理？", "旧确认框", MessageBoxIcon.Question,
                "F.ui.labResult.setText('已确认');",
                "F.ui.labResult.setText('已取消');"));
        }
    }
}
