using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using FineUI.Core.Examples.Pages.DataModel.Models;

namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class TreeNodeInfoModel : BaseModel
    {
        public void OnGet()
        {

        }

        [BindProperty]
        public IList<TreeNodeInfo> CheckedNodes { get; set; }


        // 后台可以直接通过Tree1.GetCheckedNodes()获取复选框选中的节点，参考示例：导航->树控件->复选框->复选框
        // 这个示例只是为了演示数据绑定的用法（BindProperty - CheckedNodes）
        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName != "GetCheckedNodes")
            {
                return;
            }

            if (CheckedNodes != null && CheckedNodes.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("复选框选中的节点：");
                sb.Append("<ul>");

                foreach (TreeNodeInfo checkedNode in CheckedNodes)
                {
                    sb.AppendFormat("<li>{0}（{1}）</li>", checkedNode.NodeText, checkedNode.NodeId);
                }

                sb.Append("</ul>");

                labResult.Text = sb.ToString();
            }
            else
            {
                labResult.Text = "没有复选框选中的节点";
            }

        }
    }
}