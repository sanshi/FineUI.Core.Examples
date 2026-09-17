using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeNodeClickContextMenuModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Tree1_NodeClick")
            {
                string nodeId = e.EventArguments;
                labResult.Text = String.Format("你点击了树节点：{0}（{1}）", nodeId, Tree1.FindNode(nodeId).Text);
            }
        }


    }
}