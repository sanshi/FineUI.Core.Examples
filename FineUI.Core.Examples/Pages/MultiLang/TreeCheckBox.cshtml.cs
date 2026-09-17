using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.MultiLang
{
    public partial class TreeCheckBoxModel : BaseMultilangModel
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
                sb.Append(_R("复选框选中的节点："));
                sb.Append("<ul>");

                foreach (TreeNode node in nodes)
                {
                    sb.Append(_R("<li>{0}（{1}）</li>", node.NodeID, node.Text));
                    //sb.AppendFormat(_R("<li>{0}（{1}）</li>"), node.NodeID, node.Text);
                }

                sb.Append("</ul>");
                
                labResult.Text = sb.ToString();
            }
            else
            {
                labResult.Text = _R("没有复选框选中的节点");
            }

        }

    }
}