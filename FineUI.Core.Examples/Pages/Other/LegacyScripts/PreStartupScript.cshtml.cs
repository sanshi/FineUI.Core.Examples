using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class PreStartupScriptModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 保留已废弃的首屏写法，演示先准备数据、再由启动脚本读取；回发不调用预启动接口。
#pragma warning disable CS0618
                FineUI.Core.PageContext.RegisterPreStartupScript("window.legacyOrder = ['首屏预启动'];");
#pragma warning restore CS0618
                RegisterStartupScript("legacyOrder.push('首屏启动'); F.ui.labResult.setText(legacyOrder.join(' → '));");
            }
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            RegisterStartupScript("legacyOrder.push('回发启动'); F.ui.labResult.setText(legacyOrder.join(' → '));");
        }
    }
}
