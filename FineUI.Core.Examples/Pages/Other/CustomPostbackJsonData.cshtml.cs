using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    // 独立 HTTP 响应入口：不渲染 FineUI 页面，也不初始化页面主题等配置。
    public class CustomPostbackJsonDataModel : PageModel
    {
        // 本入口只接收 POST；GET 不应返回空白页面。
        public IActionResult OnGet()
        {
            Response.Headers["Allow"] = "POST";
            return StatusCode(405);
        }

        public IActionResult OnPostProcess(string text1)
        {
            return new JsonResult(new { type = "enter", text = text1 + " - server" });
        }
    }
}
