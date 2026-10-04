using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples
{
    public static class UrlUtil
    {
        // 使用当前请求的协议、主机及应用根路径，供页面和文件服务共同使用。
        public static string GetAbsoluteUrl(HttpRequest request, IUrlHelper url, string virtualPath)
        {
            var builder = new UriBuilder(request.GetDisplayUrl())
            {
                Path = url.Content(virtualPath),
                Query = null
            };
            return builder.ToString();
        }
    }
}
