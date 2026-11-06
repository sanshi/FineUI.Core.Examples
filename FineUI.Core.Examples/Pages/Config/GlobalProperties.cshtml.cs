using System;
using System.Collections.Generic;

namespace FineUI.Core.Examples.Pages.Config
{
    public partial class GlobalPropertiesModel : PageBase
    {
        /// <summary>
        /// 每行对应 appsettings.json 中 FineUI 节的一个配置键。初始值来自空配置，
        /// 例如示例站已开启 RazorForms，这里仍展示 EnableRazorForms 的初始值 false。
        /// </summary>
        public IReadOnlyList<PropertyRow> Properties => Catalog;

        private static readonly IReadOnlyList<PropertyRow> Catalog = BuildCatalog();

        public sealed record PropertyRow(string Category, string Name, string Description, string ValueType,
            object InitialValue, string Options, object Example, string Note, bool Obsolete);

        private sealed record Guide(string Name, string Description, string Options = null,
            object Example = null, string Note = null);

        private static IReadOnlyList<PropertyRow> BuildCatalog()
        {
            // 按配置指南的分组展示迁移说明，兼顾未标记 Obsolete 的旧键，例如 EnableLargeMode。
            const string obsoleteCategory = "已废弃配置";
            var defaults = new ConfigSection();
            var rows = new List<PropertyRow>();

            void AddGroup(string category, params Guide[] guides)
            {
                foreach (var guide in guides)
                {
                    var property = typeof(ConfigSection).GetProperty(guide.Name);
                    var initialValue = property.GetValue(defaults);
                    var valueType = Type.GetTypeCode(property.PropertyType) switch
                    {
                        TypeCode.Boolean => "布尔值",
                        TypeCode.Int32 => "整数",
                        TypeCode.String => "字符串",
                        _ when property.PropertyType == typeof(string[]) => "字符串数组",
                        _ => property.PropertyType.Name
                    };

                    rows.Add(new PropertyRow(
                        category, guide.Name, guide.Description, valueType, initialValue,
                        guide.Options ?? "true、false", guide.Example ?? initialValue, guide.Note,
                        category == obsoleteCategory));
                }
            }

            AddGroup("主题与显示",
                new(nameof(ConfigSection.Theme), "选择页面使用的内置主题。",
                    "Pure_Black、Pure_Blue、Pure_Green、Pure_Orange、Pure_Purple、Cupertino、Start、Dark_Hive、Flick、South_Street",
                    Example: "Pure_Blue",
                    Note: "设置 CustomTheme 后优先加载自定义主题。"),
                new(nameof(ConfigSection.DisplayMode), "调整控件的字号和间距。",
                    "Normal（标准）、Compact（紧凑）、Small（小字）、Large（大字）、LargeSpace（大间距）",
                    Example: "Large"),
                new(nameof(ConfigSection.Language), "选择控件提示、按钮和分页栏使用的内置语言。",
                    "zh_CN（简体中文）、zh_TW（繁体中文）、en（英语）",
                    Example: "en"),
                new(nameof(ConfigSection.MobileAdaption), "为移动浏览器输出视口设置，让页面按设备宽度显示。",
                    Example: true),
                new(nameof(ConfigSection.EnableAnimation), "启用支持动画的控件过渡效果。",
                    Example: true),
                new(nameof(ConfigSection.TabStripAnimationType), "选择切换选项卡时的动画。",
                    "SlideX（水平滑动）、SlideY（垂直滑动）、Fade（淡出）、Pop（弹出）、Flip（翻转）",
                    Example: "Fade",
                    Note: "配合 EnableAnimation 使用。Undefined 表示未指定动画类型。"),
                new(nameof(ConfigSection.CustomScrollbar), "在支持的桌面浏览器中使用自定义滚动条样式。",
                    Example: true),
                new(nameof(ConfigSection.EnableDefaultCorner), "使用主题定义的控件圆角。",
                    Example: false));

            AddGroup("应用与资源",
                new(nameof(ConfigSection.EnableRazorForms), "为 Razor 页面启用 RazorForms 生命周期和后台控件事件。",
                    Example: true,
                    Note: "在应用启动时读取。页面模型或基类可用 [NoRazorForms] 单独关闭。"),
                new(nameof(ConfigSection.DebugMode), "缩进首屏 FineUIPageConfig JSON，方便查看控件配置。",
                    Example: true,
                    Note: "回发响应仍使用紧凑 JSON。"),
                new(nameof(ConfigSection.BaseUrl), "指定解析应用相对地址时使用的根路径。",
                    "应用根路径，例如 /portal/",
                    Example: "/portal/",
                    Note: "留空时取当前请求的 PathBase，部署在站点根目录时使用 /。"),
                new(nameof(ConfigSection.ContentUTF8), "在 HTML 页头声明 UTF-8 字符编码。"),
                new(nameof(ConfigSection.JSLibrary), "选择是否随 FineUI 脚本一起加载 jQuery。",
                    "All（内含 jQuery）、NoJQ（自行引入 jQuery）",
                    Example: "NoJQ",
                    Note: "选择 NoJQ 时须先加载 jQuery。旧枚举 JQv1 已废弃，当前使用与 All 相同的脚本。"),
                new(nameof(ConfigSection.IconBasePath), "指定内置图标名称对应的图片根目录。",
                    "图标目录路径",
                    Example: "~/res/icon/"),
                new(nameof(ConfigSection.CustomTheme), "使用指定名称的自定义主题。",
                    "主题目录名，例如 custom_default",
                    Example: "custom_default",
                    Note: "从 CustomThemeBasePath 下的同名目录加载 theme.css。留空时使用 Theme。"),
                new(nameof(ConfigSection.CustomThemeBasePath), "指定存放自定义主题的根目录。",
                    "主题根目录路径",
                    Example: "~/res/themes/"),
                new(nameof(ConfigSection.CustomLanguage), "加载自定义语言脚本，覆盖内置语言资源选择。",
                    "语言脚本的文件名，无需 .js 后缀",
                    Example: "my_language",
                    Note: "例如 my_language 对应 CustomLanguageBasePath 下的 my_language.js。"),
                new(nameof(ConfigSection.CustomLanguageBasePath), "指定存放自定义语言脚本的根目录。",
                    "语言文件目录路径",
                    Example: "~/res/languages/"),
                new(nameof(ConfigSection.IP2), "选择企业版服务器授权查找 IP 地址时使用的主机名。",
                    Example: false,
                    Note: "true 使用请求 URL 的主机名，false 使用系统主机名。解析失败时还会检查连接地址和本机网卡。"),
                new(nameof(ConfigSection.ServerIP), "指定企业版服务器授权优先使用的本机 IP 地址。",
                    "本机有效的非回环 IP 地址，或空字符串",
                    Example: "10.200.16.116",
                    Note: "留空时自动选择地址。无效地址或回环地址不会覆盖自动选择结果。"));

            AddGroup("表单与布局",
                new(nameof(ConfigSection.FormLabelAlign), "设置表单字段标签的位置。",
                    "Left（左对齐）、Right（右对齐）、Top（位于上方）",
                    Example: "Right"),
                new(nameof(ConfigSection.FormLabelWidth), "设置表单字段标签的宽度。",
                    "整数，单位为像素",
                    Example: 120),
                new(nameof(ConfigSection.FormLabelSeparator), "设置字段标签文字与输入区域之间的分隔文本。",
                    "字符串，可留空",
                    Example: "："),
                new(nameof(ConfigSection.FormRedStarPosition), "设置必填字段红色星号的位置。",
                    "AfterText（文字后）、BeforeText（文字前）、AfterSeparator（分隔符后）",
                    Example: "BeforeText"),
                new(nameof(ConfigSection.FormMessageTarget), "选择字段校验失败时显示提示的位置。",
                    "Side（右侧图标）、Qtip（浮动提示）、Title（浏览器提示）、None（隐藏提示）",
                    Example: "Qtip",
                    Note: "Under 是保留枚举值，当前未实现字段下方提示。"),
                new(nameof(ConfigSection.FormOffsetRight), "设置表单字段右侧预留的宽度。",
                    "整数，单位为像素",
                    Example: 8),
                new(nameof(ConfigSection.FormRowItemSpace), "设置同一表单行中相邻控件的间距。",
                    "整数，单位为像素",
                    Example: 12),
                new(nameof(ConfigSection.ToolbarItemSpace), "设置工具栏中相邻项目的间距。",
                    "整数，单位为像素",
                    Example: 12),
                new(nameof(ConfigSection.TabStripTabSpace), "设置相邻选项卡标题之间的间距。",
                    "整数，单位为像素",
                    Example: 8),
                new(nameof(ConfigSection.FormDotSeparator), "设置数字输入框在浏览器中显示的小数分隔符。",
                    "分隔字符，例如 . 或 ,",
                    Example: ",",
                    Note: "未配置时配置属性为 null，客户端默认使用点号（.）。"),
                new(nameof(ConfigSection.FormCommaSeparator), "设置数字输入框在浏览器中显示的千分位分隔符。",
                    "分隔字符，例如逗号或空格",
                    Example: " ",
                    Note: "未配置时配置属性为 null，客户端默认使用逗号（,）。"),
                new(nameof(ConfigSection.NumberBoxNoWheel), "禁止用鼠标滚轮改变数字输入框的值。",
                    Example: true),
                new(nameof(ConfigSection.EnableFormChangeConfirm), "在离开已修改的表单时请求确认，避免丢失输入。",
                    Example: true),
                new(nameof(ConfigSection.MessageBoxDisplayProgress), "显示通知对话框（Notify）自动关闭前的倒计时进度条。",
                    Example: false));

            AddGroup("回发与加载提示",
                new(nameof(ConfigSection.AjaxTimeout), "设置浏览器等待 AJAX 请求完成的超时时间。",
                    "正整数，单位为秒；0 表示不设置超时",
                    Example: 60),
                new(nameof(ConfigSection.EnableAjaxLoading), "发起 AJAX 回发时显示加载提示。",
                    Example: true),
                new(nameof(ConfigSection.AjaxLoadingType), "选择 AJAX 回发期间的提示样式。",
                    "Default（页面顶部提示）、Mask（页面遮罩）",
                    Example: "Mask"),
                new(nameof(ConfigSection.AjaxLoadingText), "设置页面顶部 AJAX 加载提示的文字。",
                    "提示文字，空字符串使用当前语言的加载提示",
                    Example: "正在查询…"),
                new(nameof(ConfigSection.ShowAjaxLoadingMaskText), "使用 Mask 提示时，在遮罩上显示加载文字。",
                    Example: true),
                new(nameof(ConfigSection.AjaxLoadingMaskText), "设置 AJAX 加载遮罩上的文字。",
                    "提示文字，空字符串使用当前语言的加载提示",
                    Example: "正在保存…",
                    Note: "AjaxLoadingType 为 Mask 且 ShowAjaxLoadingMaskText 为 true 时可见。"),
                new(nameof(ConfigSection.SimpleError), "服务器返回带标题的 HTML 错误页时，用简短提示显示错误。",
                    Example: true),
                new(nameof(ConfigSection.EnableIFrameLoading), "加载内嵌页面（IFrame）时显示加载提示。"),
                new(nameof(ConfigSection.LoadingCSSNumber), "选择页面加载时使用的 CSS 动画。",
                    "1～6 对应内置动画，0 改用加载图片",
                    Example: 3),
                new(nameof(ConfigSection.LoadingImageNumber), "选择内置加载图片的编号。",
                    "1～30 的整数",
                    Example: 2,
                    Note: "LoadingCSSNumber 为 0 且 LoadingImageUrl 留空时使用。"),
                new(nameof(ConfigSection.LoadingImageUrl), "指定用来替代内置加载图片的地址。",
                    "图片 URL 或应用相对路径",
                    Example: "~/images/loading.gif",
                    Note: "将 LoadingCSSNumber 设为 0 后生效。留空时使用内置加载图片。"),
                new(nameof(ConfigSection.CustomThemeEmbedLoadingImage), "指定自定义主题使用的内置加载图片配色文件。",
                    "内置 GIF 文件名，例如 ffffff_444444.gif",
                    Example: "ffffff_444444.gif",
                    Note: "在自定义主题下使用内置加载图片时生效。自备图片使用 LoadingImageUrl。"));

            AddGroup("表格与分页",
                new(nameof(ConfigSection.GridColumnWidth), "设置未单独指定宽度的表格列宽。",
                    "整数，单位为像素",
                    Example: 160),
                new(nameof(ConfigSection.GridMinColumnWidth), "设置表格列的默认最小宽度。",
                    "整数，单位为像素",
                    Example: 48),
                new(nameof(ConfigSection.GridHeaderSortIconVisible), "为可排序的列始终显示表头排序图标。"),
                new(nameof(ConfigSection.GridHeaderFilterIconVisible), "为可筛选的列始终显示表头筛选图标。"),
                new(nameof(ConfigSection.GridPagingToolbarVisible), "在未启用分页时也显示表格底部工具栏。",
                    Example: false),
                new(nameof(ConfigSection.GridPagingType), "选择分页栏使用的翻页控件。",
                    "Arrow、NumberBox、ArrowNumberBox、NumberButton、ArrowNumberButton",
                    Example: "Arrow",
                    Note: "Arrow 表示翻页箭头，NumberBox 是页码输入框，NumberButton 是页码按钮。组合名称同时显示两种控件。"),
                new(nameof(ConfigSection.GridPagerAlignRight), "将分页按钮放在工具栏的右侧。",
                    Example: false),
                new(nameof(ConfigSection.GridPagerAutoSimpleMode), "分页栏宽度不足时自动切换为简洁分页。",
                    Example: true,
                    Note: "简洁分页显示上一页、当前页和总页数、下一页。"),
                new(nameof(ConfigSection.GridShowPagingMessage), "在表格工具栏显示当前分页信息。"),
                new(nameof(ConfigSection.GridShowSelectionMessage), "在表格工具栏显示选中行的信息。"),
                new(nameof(ConfigSection.GridTextSelection), "允许用户选中并复制表格单元格中的文字。",
                    Example: true),
                new(nameof(ConfigSection.GridShowEmptyIconText), "表格没有数据且未配置 EmptyText 时，显示内置空状态图标和文字。",
                    Example: true),
                new(nameof(ConfigSection.GridShowTooltipWhenTruncated), "仅在单元格文本被截断时显示浮动提示。",
                    Example: true,
                    Note: "适用于使用 Qtip 提示的列。"),
                new(nameof(ConfigSection.GridStoreDateAsString), "将日期列的客户端数据保存为格式化字符串。",
                    Example: true,
                    Note: "用于 FieldType 为 Date 的列。"),
                new(nameof(ConfigSection.GridConvertLongToString), "将表格数据源中的长整型转为字符串，避免浏览器丢失整数精度。"),
                new(nameof(ConfigSection.GridStrictColumnIDValidation), "在客户端发现重复列标识符时抛出异常。",
                    Example: true,
                    Note: "默认仅警告。RazorForms 服务端始终拒绝重复的显式 ColumnID。"));

            AddGroup("内容与脚本安全",
                new(nameof(ConfigSection.EncodeText), "对控件的普通文本进行 HTML 编码。",
                    Example: true),
                new(nameof(ConfigSection.AllowDangerousRawTag), "兼容旧字符串中的 <raw> 标记，将标记内容作为原始内容处理。",
                    Example: false,
                    Note: "新页面可使用控件的 RawHtml 属性明确声明可信 HTML。"),
                new(nameof(ConfigSection.AllowDangerousScriptTag), "兼容旧字符串中的 <script> 标记，将标记内容作为脚本表达式处理。",
                    Example: false,
                    Note: "仅适用于可信的旧业务代码；严格脚本策略仍会限制表达式执行。"),
                new(nameof(ConfigSection.AllowDangerousJavaScriptUrl), "允许旧链接属性使用 javascript: 地址。",
                    Example: false,
                    Note: "例如 HyperLink.NavigateUrl。设为 false 后拒绝这类地址；页面的 CSP 仍然生效。"),
                new(nameof(ConfigSection.CspScripts), "输出内容安全策略（CSP）的 script-src 响应头，限制脚本来源与执行方式。",
                    Example: true,
                    Note: "开启后按同源、来源清单、nonce 和动态执行等配置组合出页面策略。"),
                new(nameof(ConfigSection.CspScriptsAllowSelf), "在脚本策略中允许加载与页面同源的脚本。",
                    Example: true),
                new(nameof(ConfigSection.CspScriptsAllowUrls), "在脚本策略中加入指定的脚本来源。",
                    "字符串数组，每项填写一个允许的来源",
                    Example: new[] { "https://cdn.example.com" },
                    Note: "在 CspScripts 开启时生效；未设置时不追加来源。"),
                new(nameof(ConfigSection.CspScriptsAllowNonce), "允许执行带本次请求 nonce 的可信模板内联脚本。",
                    Example: true,
                    Note: "在 CspScripts 开启时生效。框架为可信模板脚本添加与响应头匹配的 nonce。"),
                new(nameof(ConfigSection.CspScriptsAllowUnsafeEval), "在脚本策略中允许 eval 或 new Function 动态编译脚本。",
                    Example: false,
                    Note: "开启 CspScripts 时生效。旧业务或第三方库确实需要动态编译时才开启。"),
                new(nameof(ConfigSection.CspScriptsAllowUnsafeInline), "将 unsafe-inline 加入脚本策略，允许内联脚本。",
                    Example: false,
                    Note: "开启 CspScripts 时生效。可信模板内联脚本优先使用 nonce。"));

            // 旧项目仍可能用这些键。这里只列出名称和迁移办法，详情中不生成旧配置的示例。
#pragma warning disable CS0618
            AddGroup(obsoleteCategory,
                new(nameof(ConfigSection.EnableLargeMode), "启用大字模式的旧开关。",
                    Note: "大字模式请改用 \"DisplayMode\": \"Large\"。旧开关仅在 DisplayMode 为 Normal 时生效，两个旧开关同时开启时优先使用大字模式。"),
                new(nameof(ConfigSection.EnableCompactMode), "启用紧凑布局的旧开关。",
                    Note: "紧凑布局请改用 \"DisplayMode\": \"Compact\"。旧开关仅在 DisplayMode 为 Normal 且未开启 EnableLargeMode 时生效。"),
                new(nameof(ConfigSection.EnableWebForms), "RazorForms 模式的旧配置名。",
                    Note: "迁移到 EnableRazorForms。兼容期间任一开关为 true 即启用 RazorForms。"),
                new(nameof(ConfigSection.ExcludedURLs), "按请求路径排除 FineUI 中间件处理。",
                    "字符串数组，每项是匹配请求路径的正则表达式",
                    Note: "旧配置仍生效。普通文件和导出响应已自动跳过，仅需为特别大的独立 HTML 页面考虑保留。"),
                new(nameof(ConfigSection.EnableShim), "旧版浏览器防止 ActiveX 或 Flash 遮挡弹窗的兼容开关。",
                    Note: "可以从配置文件中删除。"),
                new(nameof(ConfigSection.IEEdge), "向旧版 Internet Explorer 声明 Edge 文档模式的开关。",
                    Note: "可以从配置文件中删除。"));
#pragma warning restore CS0618

            return rows.AsReadOnly();
        }
    }
}
