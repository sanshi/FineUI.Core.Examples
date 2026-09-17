using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeSmartModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ResolveTreeNodes(Tree1.Nodes);
            }
        }


        /// <summary>
        /// Ϊÿ�����ڵ�����Tooltip
        /// </summary>
        /// <param name="nodes"></param>
        private void ResolveTreeNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.ToolTip = node.Text;

                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    ResolveTreeNodes(node.Nodes);
                }
            }
        }

    }
}