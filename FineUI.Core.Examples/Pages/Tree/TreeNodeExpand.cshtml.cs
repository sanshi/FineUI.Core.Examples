using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeNodeExpandModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Tree1_NodeExpand(object sender, TreeNodeEventArgs e)
        {
            labResult.Text = String.Format("展开节点：{0}（{1}）", e.NodeID, e.Node.Text);
        }

        protected void Tree1_NodeCollapse(object sender, TreeNodeEventArgs e)
        {
            labResult.Text = String.Format("折叠节点：{0}（{1}）", e.NodeID, e.Node.Text);
        }

    }
}