using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class CheckBoxDisabledModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
