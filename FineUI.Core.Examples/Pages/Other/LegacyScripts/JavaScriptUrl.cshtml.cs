using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class JavaScriptUrlModel : LegacyScriptPageBase
    {
        protected override void ConfigureLegacyPage()
        {
            // 项目全局禁用脚本链接；只有这个兼容示例在每轮恢复输入后重新开启。
            PageManager.Instance.AllowDangerousJavaScriptUrl = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hlxOpen.NavigateUrl = "javascript:" + Window1.GetShowReference(
                    Url.Content("~/Other/LegacyScripts/Child"), "旧脚本链接");
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            labResult.Text = "链接已通过回发，仍可再次打开窗口";
        }
    }
}
