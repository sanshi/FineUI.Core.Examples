using System;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 首屏公共配置：读取当前用户偏好，页面可以在后台处理方法中进一步覆盖。
    /// </summary>
    public static class AppPageManagerInitializer
    {
        public static void Initialize(PageManager pm, HttpRequest request)
        {
            ApplyTheme(pm, request);
            ApplyLanguage(pm, request);
            ApplyLoading(pm, request);
            ApplyDisplayMode(pm, request);
            ApplyEditionConstraints(pm, request);
        }

        private static void ApplyTheme(PageManager pm, HttpRequest request)
        {
            var themeName = request.Cookies["Theme"];
            if (string.IsNullOrEmpty(themeName))
            {
                return;
            }

            if (Enum.TryParse(themeName, true, out Theme theme)
                && Enum.IsDefined(typeof(Theme), theme))
            {
                // 切换回内置主题时，先清空自定义主题。
                pm.CustomTheme = string.Empty;
                pm.Theme = theme;
            }
            else
            {
                pm.CustomTheme = themeName;
            }
        }

        private static void ApplyLanguage(PageManager pm, HttpRequest request)
        {
            var languageName = request.Cookies["Language"];
            if (string.IsNullOrEmpty(languageName))
            {
                return;
            }

            if (Enum.TryParse(languageName, true, out Language language)
                && Enum.IsDefined(typeof(Language), language))
            {
                pm.Language = language;
            }
            else
            {
                pm.CustomLanguage = languageName;
            }
        }

        private static void ApplyLoading(PageManager pm, HttpRequest request)
        {
            if (int.TryParse(request.Cookies["Loading"], out int loadingNumber))
            {
                pm.LoadingImageNumber = loadingNumber;
                // 选择 GIF 动画时先关闭 CSS 动画；下面的 CSS 配置可以再次覆盖。
                pm.LoadingCSSNumber = 0;
            }

            if (int.TryParse(request.Cookies["Loading_CSS"], out int loadingCSSNumber))
            {
                pm.LoadingCSSNumber = loadingCSSNumber;
            }
        }

        private static void ApplyDisplayMode(PageManager pm, HttpRequest request)
        {
            var modeName = request.Cookies["DisplayMode"];
            if (string.IsNullOrEmpty(modeName))
            {
                return;
            }

            if (Enum.TryParse(modeName, true, out DisplayMode mode)
                && Enum.IsDefined(typeof(DisplayMode), mode))
            {
                pm.DisplayMode = mode;
            }
            else
            {
                pm.DisplayMode = DisplayMode.Normal;
            }
        }

        /// <summary>
        /// 初始化社区版及“仅显示社区版示例”的配置，单页可以随后覆盖。
        /// </summary>
        private static void ApplyEditionConstraints(PageManager pm, HttpRequest request)
        {
            bool.TryParse(request.Cookies["ShowOnlyCommunity"], out bool showOnlyCommunity);
            if (showOnlyCommunity || Constants.IS_COMMUNITY_EDITION)
            {
                pm.EnableAnimation = false;
                pm.MobileAdaption = false;
            }
        }
    }
}
