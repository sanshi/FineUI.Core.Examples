using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeReloadModel : BaseModel
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


        protected void btnUpdateNode_Click(object sender, EventArgs e)
        {
            var source = hfDataSource.Text == "source1" ? GetSource2() : GetSource1();
            hfDataSource.Text = (hfDataSource.Text == "source1") ? "source2" : "source1";



            RegisterStartupScript(Tree1.GetLoadDataReference("zhumadian", source));
            // 展开更新后的节点
            RegisterStartupScript(Tree1.GetExpandNodeReference("zhumadian"));

        }


        private List<TreeNode> GetSource2()
        {
            List<TreeNode> nodes = new List<TreeNode>();

            TreeNode node = null;

            node = new TreeNode();
            node.Text = "遂平县";
            node.Leaf = false;
            node.NodeID = "suiping";
            nodes.Add(node);

            var suipingNodes = node.Nodes;
            node = new TreeNode();
            node.Text = "槐树乡";
            node.Leaf = false;
            node.NodeID = "huaishu";
            suipingNodes.Add(node);

            var huaishuNodes = node.Nodes;
            node = new TreeNode();
            node.Text = "陈庄村";
            node.Leaf = true;
            node.NodeID = "chenzhuang";
            huaishuNodes.Add(node);

            node = new TreeNode();
            node.Text = "王老庄";
            node.Leaf = true;
            node.NodeID = "wanglaozhuang";
            huaishuNodes.Add(node);

            node = new TreeNode();
            node.Text = "嵖岈山乡";
            node.Leaf = true;
            node.NodeID = "chayashan";
            suipingNodes.Add(node);

            node = new TreeNode();
            node.Text = "西平县";
            node.Leaf = true;
            node.NodeID = "xiping";
            nodes.Add(node);

            return nodes;
        }

        private List<TreeNode> GetSource1()
        {
            List<TreeNode> nodes = new List<TreeNode>();

            TreeNode node = null;

            node = new TreeNode();
            node.Text = "平舆县";
            node.Leaf = true;
            node.NodeID = "pingyu";
            nodes.Add(node);

            node = new TreeNode();
            node.Text = "汝南县";
            node.Leaf = true;
            node.NodeID = "runan";
            nodes.Add(node);

            node = new TreeNode();
            node.Text = "新蔡县";
            node.Leaf = true;
            node.NodeID = "xincai";
            nodes.Add(node);

            return nodes;
        }

    }
}