namespace FineUI.Core.Examples
{
    /// <summary>
    /// 第三方编辑器兼容页：UEditor 等编辑器需要内联事件和动态执行脚本，
    /// 这些页面通过公共视图入口关闭 CSP。仅实际加载编辑器的页面继承此类，
    /// 打开编辑窗口的外层页面仍使用全局严格策略。
    /// </summary>
    public abstract class BaseThirdPartyEditorModel : BaseModel
    {
    }
}
