using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class ExcelDownloadFileModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public Task<IActionResult> OnPostExportToExcel()
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
