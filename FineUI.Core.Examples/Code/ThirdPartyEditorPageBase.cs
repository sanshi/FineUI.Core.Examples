using System;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 第三方编辑器兼容页：UEditor 等编辑器需要内联事件和动态执行脚本，
    /// 这些页面在首次打开时通过 Page_Init 关闭本页 CSP。仅实际加载编辑器的页面继承此类，
    /// 打开编辑窗口的外层页面仍使用全局严格策略。
    /// </summary>
    public abstract class ThirdPartyEditorPageBase : PageBase
    {
        public void Page_Init(object sender, EventArgs e)
        {
            // 编辑器需要执行自身的内联事件和动态脚本；只关闭当前编辑器页的 CSP 响应头。
            // 打开编辑窗口的外层页面不继承本类，继续使用全局严格策略。
            PageManager.Instance.CspScripts = false;
        }
    }
}
