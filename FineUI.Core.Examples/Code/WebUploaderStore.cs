using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace FineUI.Core.Examples
{
    /// <summary>
    /// WebUploader 示例的「已上传文件清单」存储：按 owner（一个会话键）分别存一份。
    /// 上传端点、页面的删除按钮、表格绑定都读同一份，所以收在这里而不是各页各写一遍。
    ///
    /// 特别注意：真实开发中不要在会话里放大量数据，否则严重影响服务器性能；示例这么写只为免去数据库。
    /// </summary>
    public static class WebUploaderStore
    {
        /// <summary>
        /// owner 的固定前缀。五个 WebUploader 示例页的会话键都以它开头，用来挡住「实参写反」——
        /// DeleteRow(owner, rowId) 两个形参都是 string，写反编译器不报错，
        /// 而未知 owner 在下面只会「建个空清单、什么都不删、不抛异常」，是最难查的那种静默失败。
        /// </summary>
        public const string OWNER_PREFIX = "webuploader.";

        //:: 挡住写反的实参：行标识（GUID）不可能以 webuploader. 开头
        private static void EnsureOwner(string owner)
        {
            if (owner == null || !owner.StartsWith(OWNER_PREFIX))
            {
                throw new ArgumentException(
                    "owner 必须是以 " + OWNER_PREFIX + " 开头的会话键（实参顺序是 owner 在前、行标识在后）：" + owner, "owner");
            }
        }

        /// <summary>
        /// 取某个 owner 的文件清单，没有就先建一个空的。
        /// </summary>
        public static JArray GetSourceData(ISession session, string owner)
        {
            EnsureOwner(owner);
            if (session.GetObject<JArray>(owner) == null)
            {
                session.SetObject<JArray>(owner, new JArray());
            }
            return session.GetObject<JArray>(owner);
        }

        /// <summary>
        /// 回写某个 owner 的文件清单。只给 Delete 内部用——不公开，避免多出一个
        /// 「拿客户端可控的 owner 覆盖任意会话键」的原语。
        /// </summary>
        private static void SetSourceData(ISession session, string owner, JArray source)
        {
            session.SetObject<JArray>(owner, source);
        }

        /// <summary>
        /// 按行标识删除一条记录，并尽力删掉对应的物理文件（删不掉就算了，不影响清单）。
        /// </summary>
        public static void Delete(ISession session, string owner, string rowId)
        {
            EnsureOwner(owner);
            JArray source = GetSourceData(session, owner);

            for (int i = 0, count = source.Count; i < count; i++)
            {
                JObject item = source[i] as JObject;
                if (item.Value<string>("id") == rowId)
                {
                    try
                    {
                        string savedName = item.Value<string>("savedName");
                        File.Delete(UploadStorage.GetUploadFilePath(savedName));
                    }
                    catch (Exception)
                    {
                        // 尝试删除物理文件失败，不做处理
                    }

                    source.RemoveAt(i);
                    break;
                }
            }

            SetSourceData(session, owner, source);
        }
    }
}
