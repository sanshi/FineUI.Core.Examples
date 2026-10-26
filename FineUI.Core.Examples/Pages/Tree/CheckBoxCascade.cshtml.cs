using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class CheckBoxCascadeModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnToggleCascadeCheck_Click(object sender, EventArgs e)
        {
            // 切换时保留当前勾选结果，后续勾选再使用新规则。
            Tree1.CascadeCheck = !Tree1.CascadeCheck;
            labCascadeCheck.Text = Tree1.CascadeCheck ? "父子级联勾选：开启" : "父子级联勾选：关闭";
        }


        
        protected void btnGetCheckedValues_Click(object sender, EventArgs e)
        {
            TreeNode[] nodes = Tree1.GetCheckedNodes();
            if (nodes.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("复选框选中的节点：");
                sb.Append("<ul>");

                foreach (TreeNode node in nodes)
                {
                    sb.AppendFormat("<li>{0}（{1}）</li>", node.NodeID, node.Text);
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
