using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeContextMenuModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnToggleContextSelection_Click(object sender, EventArgs e)
        {
            // 只改变右键是否先选中节点，菜单继续作用于实际右键的节点。
            Tree1.ClickOnContextMenu = !Tree1.ClickOnContextMenu;
            labContextSelection.Text = Tree1.ClickOnContextMenu ? "右键选中节点：开启" : "右键选中节点：关闭";
        }


    }
}
