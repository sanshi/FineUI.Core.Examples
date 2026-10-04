using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.Grid
{
    // 独立 HTTP 响应入口：不渲染 FineUI 页面，也不初始化页面主题等配置。
    public class ExcelDownloadFileExportModel : PageModel
    {
        // 本入口只接收 POST；GET 不应返回空白页面。
        public IActionResult OnGet()
        {
            Response.Headers["Allow"] = "POST";
            return StatusCode(405);
        }

        public Task<IActionResult> OnPost()
        {
            //拼出文件路径。
            string path = FineUI.Core.PageContext.MapPath("~/wwwroot/res/menu.xml");

            //方案一
            //把文件内容读进 FileStream。
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            //把文件交给浏览器下载。
            return Task.FromResult<IActionResult>(new FileStreamResult(fileStream, "text/xml"));

            //// 方案一
            //return File(System.IO.File.ReadAllBytes(path), "text/xml");

            //// 方案三
            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    await stream.CopyToAsync(memory);
            //}
            //memory.Position = 0;
            //return File(memory, "text/xml");
        }
    }
}
