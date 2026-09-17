using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// WebUploader 示例页基类：在 <see cref="BaseModel"/> 之上提供「已上传文件清单」的读取与删除。
    /// 具体实现都在 <see cref="WebUploaderStore"/> 里，本类只是页面侧的门面。
    /// owner 是该页（或该页某个表格）的会话键，由页面自己声明。
    /// </summary>
    public class BaseWebUploaderModel : BaseModel
    {
        /// <summary>
        /// 取某个 owner 的已上传文件清单，没有就先建一个空的。
        /// </summary>
        protected JArray GetSourceData(string owner)
        {
            return WebUploaderStore.GetSourceData(HttpContext.Session, owner);
        }

        /// <summary>
        /// 按行标识删除一条记录（连带尽力删掉物理文件）。
        /// </summary>
        protected void DeleteRow(string owner, string rowId)
        {
            WebUploaderStore.Delete(HttpContext.Session, owner, rowId);
        }
    }
}
