using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ConfirmTextModel : LegacyScriptPageBase
    {
        protected override void ConfigureLegacyPage()
        {
            // 只有这个旧文案示例需要解析 script 标签，不改变应用和其他页面的配置。
            PageManager.Instance.AllowDangerousScriptTag = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnConfirm.ConfirmText = "确认处理：<script>F.ui.tbxName.getValue()</script>？";
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            labResult.Text = "已确认：" + tbxName.Text;
        }
    }
}
