using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;


namespace FineUI.Core.Examples.Pages.GridMove
{
    public partial class RowMoveBetween2GridModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "CheckSelected_Click")
            {
                var param = e.EventArgumentsAsJObject;

                CheckSelected_Click(param.Value<JArray>("columnNames"));
            }
        }


        protected void CheckSelected_Click(JArray columnNames)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (JObject item in columnNames)
            {
                sb.AppendFormat("<li>ID:{0} Name:{1}</li>", item.Value<string>("id"), item.Value<string>("name"));
            }
            sb.Append("</ul>");

            ShowNotify(new RawHtml("已选择列表：" + sb.ToString()), MessageBoxIcon.None);
        }

    }
}