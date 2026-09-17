using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Threading.Tasks;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 自定义请求语言提供器（默认的Cookie格式：.AspNetCore.Culture=c=zh-CN|uic=zh-CN，为了复用之前已经定义过的Cookie：Language=zh_CN）
    /// </summary>
    public class MyCustomRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var langCookie = httpContext.Request.Cookies["Language"];
            if (!String.IsNullOrEmpty(langCookie))
            {
                // 简单的转换：zh_CN -> zh-CN
                langCookie = langCookie.Replace("_", "-");
                if (langCookie == "en")
                {
                    langCookie = "en-US";
                }
            }
            else
            {
                // 默认为中文
                langCookie = "zh-CN";
            }

            var providerResultCulture = new ProviderCultureResult(langCookie);
            return Task.FromResult<ProviderCultureResult>(providerResultCulture);
        }
    }
}
