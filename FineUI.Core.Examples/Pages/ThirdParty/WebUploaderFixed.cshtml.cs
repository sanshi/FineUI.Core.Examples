using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class WebUploaderFixedModel : BaseWebUploaderModel
    {
        private static readonly string KEY_FOR_DATASOURCE_SESSION = "webuploader.webuploader_fixed";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "RebindGrid")
            {
                LoadData();
            }
            else if (e.EventName == "DeleteRow")
            {
                DeleteRow(e.EventArguments);
                LoadData();
            }
            else if (e.EventName == "DeleteRows")
            {
                foreach (string rowId in e.EventArgumentsAsJArray)
                {
                    DeleteRow(rowId);
                }
                LoadData();
            }
        }

        private void LoadData()
        {
            Grid1.DataSource = GetSourceData();
            Grid1.DataBind();
        }

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSourceData()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                JArray source = new JArray();
                JObject fileObj;

                fileObj = new JObject();
                fileObj.Add("id", "1_mulu");
                fileObj.Add("sectionname", "目录");
                fileObj.Add("name", "");
                fileObj.Add("type", "doc");
                fileObj.Add("savedName", "");
                fileObj.Add("size", "");
                fileObj.Add("status", "");
                source.Add(fileObj);

                fileObj = new JObject();
                fileObj.Add("id", "2_yinyan");
                fileObj.Add("sectionname", "引言");
                fileObj.Add("name", "");
                fileObj.Add("type", "doc");
                fileObj.Add("savedName", "");
                fileObj.Add("size", "");
                fileObj.Add("status", "");
                source.Add(fileObj);

                fileObj = new JObject();
                fileObj.Add("id", "3_zhengwen");
                fileObj.Add("sectionname", "正文");
                fileObj.Add("name", "");
                fileObj.Add("type", "doc");
                fileObj.Add("savedName", "");
                fileObj.Add("size", "");
                fileObj.Add("status", "");
                source.Add(fileObj);

                fileObj = new JObject();
                fileObj.Add("id", "4_cankao");
                fileObj.Add("sectionname", "参考文献");
                fileObj.Add("name", "");
                fileObj.Add("type", "doc");
                fileObj.Add("savedName", "");
                fileObj.Add("size", "");
                fileObj.Add("status", "");
                source.Add(fileObj);

                HttpContext.Session.SetObject<JArray>(KEY_FOR_DATASOURCE_SESSION, source);
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION);
        }


        
        private void DeleteRow(string rowId)
        {
            JArray source = GetSourceData();

            for (int i = 0, count = source.Count; i < count; i++)
            {
                JObject item = source[i] as JObject;

                if (item.Value<string>("id") == rowId)
                {

                    try
                    {
                        string savedName = item.Value<string>("savedName");
                        System.IO.File.Delete(UploadStorage.GetUploadFilePath(savedName));
                    }
                    catch (Exception)
                    {
                        // 尝试删除物理文件失败，不做处理
                    }

                    item["name"] = "";
                    item["type"] = "doc";
                    item["savedName"] = "";
                    item["size"] = null;
                    item["status"] = "";
                    break;
                }
            }

            HttpContext.Session.SetObject<JArray>(KEY_FOR_DATASOURCE_SESSION, source);
        }



    }
}
