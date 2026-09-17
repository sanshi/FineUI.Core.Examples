using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeLazyLoadModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Tree1_NodeLazyLoad(object sender, TreeNodeEventArgs e)
        {
            List<TreeNode> nodes = DynamicAppendNode(e.NodeID);

            RegisterStartupScript(Tree1.GetLoadDataReference(e.NodeID, nodes));
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


        private List<TreeNode> DynamicAppendNode(string nodeId)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            TreeNode node = null;
            switch (nodeId)
            {
                case "zhumadian":
                    node = new TreeNode();
                    node.Text = "遂平县（延迟加载）";
                    node.Leaf = false;
                    node.NodeID = "suiping";
                    nodes.Add(node);

                    node = new TreeNode();
                    node.Text = "西平县";
                    node.Leaf = true;
                    node.NodeID = "xiping";
                    nodes.Add(node);
                    break;
                case "suiping":
                    node = new TreeNode();
                    node.Text = "槐树乡（延迟加载）";
                    node.Leaf = false;
                    node.NodeID = "huaishu";
                    nodes.Add(node);

                    node = new TreeNode();
                    node.Text = "嵖岈山乡";
                    node.Leaf = true;
                    node.NodeID = "chayashan";
                    nodes.Add(node);
                    break;
                case "huaishu":
                    node = new TreeNode();
                    node.Text = "陈庄村";
                    node.Leaf = true;
                    node.NodeID = "chenzhuang";
                    nodes.Add(node);

                    node = new TreeNode();
                    node.Text = "王老庄";
                    node.Leaf = true;
                    node.NodeID = "wanglaozhuang";
                    nodes.Add(node);
                    break;
            }

            return nodes;
        }

    }
}