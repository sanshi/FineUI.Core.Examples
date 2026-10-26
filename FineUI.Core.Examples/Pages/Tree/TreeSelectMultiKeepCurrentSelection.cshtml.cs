using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeSelectMultiKeepCurrentSelectionModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Tree1.SelectedNodeIDArray = new string[] { "zhumadian", "luohe" };
            }
        }



        protected void btnGetSelectedValues_Click(object sender, EventArgs e)
        {
            if (Tree1.SelectedNodeIDArray.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("选中的节点：");
                sb.Append("<ul>");

                foreach (string nodeId in Tree1.SelectedNodeIDArray)
                {
                    sb.AppendFormat("<li>{0}（{1}）</li>", Tree1.FindNode(nodeId).Text, nodeId);
                }

                sb.Append("</ul>");

                labResult.Text = sb.ToString();
            }
            else
            {
                labResult.Text = "没有选中的节点";
            }

        }

        protected void btnSelectOthers_Click(object sender, EventArgs e)
        {
            Tree1.SelectedNodeIDArray = Tree1.SelectedNodeIDArray.Concat(new string[] { "hefei", "huangshan" }).ToArray();
        }

        protected void btnToggleKeepSelection_Click(object sender, EventArgs e)
        {
            // 保留已有选择，只改变接下来的普通点击是否累加。
            Tree1.KeepCurrentSelection = !Tree1.KeepCurrentSelection;
            labKeepSelection.Text = Tree1.KeepCurrentSelection ? "连续点击累加选择：开启" : "连续点击累加选择：关闭";
        }

    }
}
