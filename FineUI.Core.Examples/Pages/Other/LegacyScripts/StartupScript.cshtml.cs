using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class StartupScriptModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterStartupScript("F.ui.labResult.setText('首次启动脚本已执行');");
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            // 输入包含引号或换行时仍作为文字传给脚本，避免改变脚本含义。
            RegisterStartupScript("F.ui.labResult.setText(" + JsHelper.Enquote(tbxValue.Text) + ");");
        }
    }
}
