using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeSelectModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnGetSelectedNode_Click(object sender, EventArgs e)
        {
            string selectedID = Tree1.SelectedNodeID;
            if (!String.IsNullOrEmpty(selectedID))
            {
                labResult.Text = String.Format("选中的节点：{0}（{1}）", Tree1.FindNode(selectedID).Text, selectedID);
            }
            else
            {
                labResult.Text = "没有选中节点";
            }
        }

        protected void btnSelecthefei_Click(object sender, EventArgs e)
        {
            Tree1.SelectedNodeID = "hefei";
        }
    }
}