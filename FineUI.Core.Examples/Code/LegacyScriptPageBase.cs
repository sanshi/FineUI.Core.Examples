using System;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 旧脚本兼容示例禁用 CSP 脚本限制，验证脚本字符串、内联事件和旧回调仍可执行。
    /// 仅本组示例继承此类，其他页面继续使用应用的严格 CSP。
    /// </summary>
    public abstract class LegacyScriptPageBase : PageBase
    {
        public void Page_Init(object sender, EventArgs e)
        {
            PageManager.Instance.CspScripts = false;
            ConfigureLegacyPage();
        }

        protected virtual void ConfigureLegacyPage()
        {
        }
    }
}
