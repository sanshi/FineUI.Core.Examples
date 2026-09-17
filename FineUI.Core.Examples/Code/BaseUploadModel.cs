using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// 含文件上传的示例页基类：在 <see cref="BaseModel"/> 之上补「安全上传」便捷方法
    /// （校验、保存到 wwwroot 之外、生成公共下载地址），供上传类示例页复用。
    /// 具体实现都在 <see cref="UploadStorage"/> 里，本类只是页面侧的门面。
    /// </summary>
    public class BaseUploadModel : BaseModel
    {
        /// <summary>
        /// 对上传文件做校验：扩展名白名单 + 文件大小上限。
        /// 任一项不通过即返回 false，并通过 error 输出具体原因（可直接用于 ShowNotify）。
        /// </summary>
        protected static bool ValidateUploadFile(IFormFile file, out string error)
        {
            if (file == null)
            {
                error = "文件为空！";
                return false;
            }
            return UploadStorage.Validate(file.FileName, file.Length, out error);
        }

        /// <summary>
        /// 对 FineUI 上传控件中的文件做校验（单文件场景的便捷重载）。
        /// </summary>
        protected static bool ValidateUploadFile(FileUpload fileUpload, out string error)
        {
            if (fileUpload == null || !fileUpload.HasFile)
            {
                error = "文件为空！";
                return false;
            }
            return ValidateUploadFile(fileUpload.PostedFile, out error);
        }

        /// <summary>
        /// 保存 FineUI 上传控件中的文件到 wwwroot 之外的目录，返回保存后的文件名。
        /// </summary>
        protected static string SaveUploadFile(FileUpload fileUpload)
        {
            using (var source = fileUpload.PostedFile.OpenReadStream())
            {
                return UploadStorage.Save(fileUpload.ShortFileName, source);
            }
        }

        /// <summary>
        /// 保存 IFormFile（多文件场景）到 wwwroot 之外的目录，返回保存后的文件名。
        /// </summary>
        protected static string SaveUploadFile(IFormFile file)
        {
            using (var source = file.OpenReadStream())
            {
                return UploadStorage.Save(file.FileName, source);
            }
        }

        /// <summary>
        /// 获取上传图片的访问地址（指向公共下载页 /Home/Download，带 inline=1 请求内联显示）。
        /// </summary>
        protected string GetImageUrl(string fileName)
        {
            return Url.Page("/Home/Download", new { file = fileName, inline = "1" });
        }

        /// <summary>
        /// 获取上传文件的访问地址（同一个公共下载页，不带 inline，一律作为附件下载）。
        /// 供服务端代码生成下载链接时使用；WebUploader 示例的下载链接是在页面脚本里按行拼的
        /// （那时候文件名只在客户端有），拼法与本方法等价。
        /// </summary>
        protected string GetFileUrl(string fileName)
        {
            return Url.Page("/Home/Download", new { file = fileName });
        }
    }
}
