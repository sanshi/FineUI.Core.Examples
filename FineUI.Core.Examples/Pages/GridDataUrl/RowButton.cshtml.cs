using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web;


namespace FineUI.Core.Examples.Pages.GridDataUrl
{
    public partial class RowButtonModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_CustomEvent")
            {
                var param = e.EventArgumentsAsJObject;

                string eventType = param.Value<string>("eventType");
                string eventTypeStr = String.Empty;
                if (eventType == "edit")
                {
                    eventTypeStr = "编辑";
                }
                else if (eventType == "delete")
                {
                    eventTypeStr = "删除";
                }

                ShowNotify(String.Format("你点击了第 {0} 行的 {3} 按钮，行ID：{1}，姓名：{2}",
                    param.Value<int>("rowIndex") + 1,
                    param.Value<string>("rowId"),
                    param.Value<string>("rowText"),
                    eventTypeStr));
            }
        }

    }
}