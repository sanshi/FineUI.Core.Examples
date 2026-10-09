# FineUI.Core.Examples

FineUI.Core.Examples 是 FineUI 官方完整示例项目。本仓库是该项目的唯一真相源，欢迎通过 Issue 和 Pull Request 参与技术讨论与改进。

<!-- fineui-community:start -->
## 加入 FineUI 社区

欢迎[加入 FineUI 社区](https://fineui.com/fans/)，一站式获取社区版、完整示例、空项目、快速入门和 AppBox 等配套资源，及时了解版本更新，交流控件用法与项目实践。**FineUI.Core、FineUI.Pro 与 FineUI.Java 社区版均可永久免费商用**，欢迎一起分享经验、讨论问题。
<!-- fineui-community:end -->

## 依赖方式

项目文件已声明从公共软件包仓库获取的 NuGet 包 `FineUI.Core`。正常联网构建时，包管理器会自动还原依赖。

## 生成设计时文件

安装 Node.js 后，双击仓库根目录的 `生成并监听设计时文件.bat`。脚本会先扫描页面，再持续监听 `.cshtml` 与对应后台文件的改动；按回车或关闭窗口即可停止。

只有页面声明了可生成字段的 FineUI 控件时，才会生成 `.cshtml.designer.cs`；没有控件字段的页面不会生成空的设计时文件。工具会清理已不再需要的自动生成文件，保留无法确认来源或包含手写成员的文件。

只检查文件是否需要生成、更新或清理，可在仓库根目录运行 `.\生成并监听设计时文件.bat --check`；只生成一次而不监听，可运行 `node tools/generate-designers.mjs`。

## 构建

安装 .NET 8 SDK 后，在仓库根目录运行：

```powershell
dotnet restore FineUI.Core.Examples.sln
dotnet build FineUI.Core.Examples.sln -c Release --no-restore
```

## 运行

在仓库根目录启动（首次会先还原 NuGet 包并编译）：

```powershell
dotnet run --project FineUI.Core.Examples/FineUI.Core.Examples.csproj
```

启动后打开 <http://localhost:5000/> —— 地址来自 `FineUI.Core.Examples/Properties/launchSettings.json` 里的 `FineUI.Core.Examples` 配置。

也可以用 Visual Studio 打开 `FineUI.Core.Examples.sln`，把启动配置切成 `IIS Express`（<http://localhost:61517/>）。

端口被占用时，改 `Properties/launchSettings.json` 里对应配置的 `applicationUrl` 即可。

**不需要授权文件**：本仓库引用的是公共 NuGet 包 `FineUI.Core`（社区版），社区版不做授权校验，克隆下来就能直接跑。

## 页面配置

全站默认值放在 `appsettings.json` 的 `FineUI` 节。每用户的主题 Cookie 由应用的 `AppPageManagerInitializer.Initialize(pm, request)` 读取；页面基类在 GET 处理器之前调用它。

单页配置放在页面模型的 `OnGet`（MVC 放在返回视图的 Action），使用 `PageManager.Instance`：

```csharp
var pm = PageManager.Instance;
pm.EnableWatermark = true;
pm.WatermarkText = "I❤︎FineUI";
```

配置顺序是“全站默认 → 应用公共初始化 → 单页设置 → 渲染视图”。布局保留 `@F.PageManager`、样式与脚本输出；RazorForms 中依赖控件字段的数据绑定仍放在 `Page_Load`。正常 AJAX 回发沿用已有页面状态，不重复读取 Cookie 初始化配置。

## 许可边界

本仓库中由合肥三生石上软件有限公司拥有著作权的示例或应用项目源代码采用 [MIT 许可证](LICENSE)。FineUI 各端框架源码、二进制软件包、内嵌的 FineUI.js 运行时以及 FineUI 名称、标识和商标不属于 MIT 授权范围，仍适用各自的商业或社区版许可。具体边界见 [NOTICE.md](NOTICE.md)。

## 参与贡献

请先阅读 `CONTRIBUTING.md`。安全问题请按 `SECURITY.md` 私下报告。
