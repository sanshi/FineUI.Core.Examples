using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class TreeScriptsModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Tree1.Nodes.Add(new TreeNode { NodeID = "initial", Text = "请先加载部门", Leaf = true });
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            var department = new TreeNode { NodeID = "development", Text = "研发部门", Expanded = false };
            department.Nodes.Add(new TreeNode { NodeID = "development-client", Text = "客户端组", Leaf = true });
            RegisterStartupScript(Tree1.GetLoadDataReference(new[] { department }));
        }

        protected void btnExpand_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Tree1.GetExpandNodeReference("development"));
        }
    }
}
