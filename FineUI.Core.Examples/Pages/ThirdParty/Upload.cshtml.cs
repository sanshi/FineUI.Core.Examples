using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    /// <summary>
    /// WebUploader 示例的上传入口。
    ///
    /// 上传文件保存在 App_Data/upload（wwwroot 之外，静态文件中间件够不到），浏览器无法直接访问；
    /// 读回走公共下载页 /Home/Download（下载链接在页面脚本里按行拼，不带 inline），强制
    /// application/octet-stream + Content-Disposition: attachment，所以传上来的 .html 等
    /// 只会被下载，绝不会被当页面/脚本执行。
    ///
    /// 与头像/图片上传示例共用同一个上传目录（UploadStorage.UPLOAD_DIR），也共用同一个下载入口——
    /// 区别只在调用方要不要 inline：图片示例的 GetImageUrl 带 inline=1（按 image/xxx 内联显示），
    /// 这里的下载链接不带（任意文件、恒下载）。
    /// </summary>
    // 单个上传文件的大小上限（100MB），与页面上 WebUploader 的 fileSingleSizeLimit 保持一致。
    // 不设的话 Kestrel 用默认的 30MB，示例页写着 100MB 却在 30MB 处以 413 失败。
    // 注意：Razor Pages 的过滤器只在页面模型这一级生效，加在处理方法上不起作用。
    [RequestSizeLimit(100L * 1024 * 1024)]
    [RequestFormLimits(MultipartBodyLengthLimit = 100L * 1024 * 1024)]
    public partial class UploadModel : BaseUploadModel
    {
        private static readonly string WEBUPLOADER_FIXED_SESSION_NAME = "webuploader.webuploader_fixed";


        public IActionResult OnPostProcess(IFormFile file, string owner)
        {
            //System.Threading.Thread.Sleep(2000);
            IFormFile postedFile = file;

            Response.ContentType = "text/plain";

            //string owner = Request.Form["owner"];

            if (postedFile == null)
            {
                return ResponseError();
            }

            if (String.IsNullOrEmpty(owner))
            {
                return ResponseError();
            }

            // owner 直接来自请求表单、下面会被当会话键用。不校验的话客户端能写任意会话键
            // （值是服务端造的 JArray，塞不进恶意内容，但能把别的页的会话值覆盖掉、让那页下次强转失败）。
            if (!owner.StartsWith(WebUploaderStore.OWNER_PREFIX))
            {
                return ResponseError();
            }

            // 固定槽的 owner 必须形如「webuploader.webuploader_fixed#行标识」（客户端 initUploader 拼的）。
            // 只带前缀不带 #行标识时既不是固定槽上传、也不该当普通上传落进那个键——那会给只认自己
            // 播种槽的固定页凭空多出一条记录。挡在这里而不是等分派完：晚了文件已经落盘，留下孤儿文件。
            if (owner.StartsWith(WEBUPLOADER_FIXED_SESSION_NAME)
                && !owner.StartsWith(WEBUPLOADER_FIXED_SESSION_NAME + "#"))
            {
                return ResponseError();
            }

            // 文件名完整路径
            string fileName = postedFile.FileName;
            // 保存到 App_Data/upload（wwwroot 之外），返回保存后的文件名
            string savedFileName = SaveUploadFile(postedFile);

            string shortFileName = GetFileName(fileName);
            string fileType = GetFileType(fileName);
            long fileSize = postedFile.Length;

            // 固定槽的 owner 形如「webuploader.webuploader_fixed#行标识」（客户端 initUploader 拼的）。
            // 必须带 # 才算：只判前缀的话，owner 恰好等于前缀本身时下面的 Substring 会越界。
            string fixedPrefix = WEBUPLOADER_FIXED_SESSION_NAME + "#";
            if (owner.StartsWith(fixedPrefix))
            {
                // 固定文件上传页面专用
                string fileId = owner.Substring(fixedPrefix.Length);

                // 会话清单可能还没初始化（比如先调上传端点、没打开过页面），此时按「找不到该行」处理
                JArray source = HttpContext.Session.GetObject<JArray>(WEBUPLOADER_FIXED_SESSION_NAME);
                JObject fileObj = source == null ? null : GetFileObject(source, fileId);
                if (fileObj == null)
                {
                    return ResponseError();
                }

                fileObj["name"] = shortFileName;
                fileObj["type"] = fileType;
                fileObj["savedName"] = savedFileName;
                fileObj["size"] = fileSize;
                fileObj["status"] = "uploaded";

                // 和FineUIMvc不同之处：修改Session的值后要记得保存
                HttpContext.Session.SetObject<JArray>(WEBUPLOADER_FIXED_SESSION_NAME, source);
            }
            else
            {
                JObject fileObj = new JObject();
                string fileId = Guid.NewGuid().ToString();

                fileObj.Add("name", shortFileName);
                fileObj.Add("type", fileType);
                fileObj.Add("savedName", savedFileName);
                fileObj.Add("size", fileSize);
                fileObj.Add("id", fileId);

                fileObj.Add("status", "uploaded");

                SaveToDatabase(owner, fileObj);

            }



            return Content("Success");
        }

        // 本页只是个上传端点，没有可浏览的内容。不写这个方法的话，任何 GET（包括指向
        // 旧下载地址的历史链接）都会因为找不到匹配的处理方法而变成 500。
        public IActionResult OnGet()
        {
            return NotFound();
        }

        private ActionResult ResponseError()
        {
            // 出错了
            Response.StatusCode = 500;
            return Content("No file");
        }

        private JObject GetFileObject(JArray source, string fileId)
        {
            for (int i = 0, count = source.Count; i < count; i++)
            {
                JObject item = source[i] as JObject;

                if (item.Value<string>("id") == fileId)
                {
                    return item;
                }
            }
            return null;
        }


        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private void SaveToDatabase(string sessionName, JObject fileObj)
        {
            if (HttpContext.Session.GetObject<JArray>(sessionName) == null)
            {
                HttpContext.Session.SetObject<JArray>(sessionName, new JArray());
            }

            JArray source = HttpContext.Session.GetObject<JArray>(sessionName);
            source.Add(fileObj);

            HttpContext.Session.SetObject<JArray>(sessionName, source);
        }


        private string GetFileType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            return fileType;
        }

        private string GetFileName(string fileName)
        {
            string shortFileName = fileName;
            int lastSlashIndex = shortFileName.LastIndexOf("\\");
            if (lastSlashIndex >= 0)
            {
                shortFileName = shortFileName.Substring(lastSlashIndex + 1);
            }

            return shortFileName;
        }

    }
}
