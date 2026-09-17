using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FineUI.Core.Examples.Pages.GridTree
{
    public partial class CheckBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName != "GetCheckedRows")
            {
                return;
            }

            var checkedRows = e.EventArgumentsAsJArray;
            if (checkedRows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("复选框选中的值：");
                sb.Append("<ul>");

                foreach (JObject checkedNode in checkedRows)
                {
                    sb.AppendFormat("<li>{0}（{1}）</li>", checkedNode.Value<string>("text"), checkedNode.Value<string>("id"));
                }

                sb.Append("</ul>");

                labResult.Text = sb.ToString();
            }
            else
            {
                labResult.Text = "没有复选框被选中";
            }

        }

    }
}