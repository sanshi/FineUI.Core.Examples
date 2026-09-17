using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Home
{
    // 公共下载入口：图片上传示例与 WebUploader 示例共享。
    // 读取保存在 wwwroot 之外（UploadStorage.UPLOAD_DIR）的上传文件。
    //
    // 输出形态由**调用方**声明，而不是由文件扩展名推断：
    // 不带 inline（GetFileUrl）→ 一律 application/octet-stream + attachment，浏览器只下载不渲染；
    // 带 inline=1（GetImageUrl）→ 仅当扩展名在图片白名单内才按 image/xxx 内联显示，
    // 白名单外（.html / .svg 等）降级为附件下载。
    //
    // 于是“能被内联渲染的集合”恒为那 5 种位图，与谁来请求、请求方怎么写参数无关。
    public partial class DownloadModel : BaseModel
    {
        // GET: /Home/Download?file=xxx[&inline=1]
        public IActionResult OnGet(string file, string inline)
        {
            if (String.IsNullOrEmpty(file))
            {
                return NotFound();
            }

            // 安全①：只取文件名部分，剥离任何目录信息，防止路径穿越（如 ..\..\appsettings.json）
            string safeName = Path.GetFileName(file);
            if (safeName != file)
            {
                return BadRequest();
            }

            string fullPath = UploadStorage.GetUploadFilePath(safeName);
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            // 禁止浏览器嗅探内容改写 Content-Type
            Response.Headers["X-Content-Type-Options"] = "nosniff";

            // 安全②：只有白名单内的图片、且调用方明确要求内联时，才按 image/xxx 输出。
            string imageContentType = UploadStorage.IsInlineRequested(inline) ? UploadStorage.GetImageContentType(safeName) : null;
            if (imageContentType != null)
            {
                // 不写 Content-Disposition，浏览器默认即内联
                return PhysicalFile(fullPath, imageContentType);
            }

            // 其余一律以附件下载：传上来的 .html / .svg 等不会被渲染，从根上杜绝存储型 XSS。
            // 传了 fileDownloadName，框架会自动写出 Content-Disposition（含支持 UTF-8 文件名的
            // filename*，中文名不乱码）。
            return PhysicalFile(fullPath, "application/octet-stream", safeName);
        }
    }
}
