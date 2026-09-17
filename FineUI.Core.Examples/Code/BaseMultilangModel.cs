using Microsoft.AspNetCore.Mvc.Localization;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 多语言示例页基类：在 <see cref="BaseModel"/> 之上提供「按页面模型取多语言资源」的便捷方法。
    /// 只有多语言示例页需要，所以放在这一层而不是通用页面基类里。
    /// </summary>
    public class BaseMultilangModel : BaseModel
    {
        private IHtmlLocalizer _localizer;

        /// <summary>
        /// 页面模型的多语言资源
        /// </summary>
        public IHtmlLocalizer Localizer
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = FineUI.Core.PageContext.GetLocalizer(this.GetType());
                }
                return _localizer;
            }
        }

        /// <summary>
        /// 获取页面模型的多语言资源
        /// </summary>
        public string GetResource(string name, params object[] arguments)
        {
            if (arguments.Length == 0)
            {
                return Localizer.GetString(name);
            }
            else
            {
                return Localizer.GetString(name, arguments);
            }
        }

        /// <summary>
        /// GetResource 的简写形式，页面与视图里大量使用。
        /// </summary>
        public string _R(string name, params object[] arguments)
        {
            return GetResource(name, arguments);
        }
    }
}
