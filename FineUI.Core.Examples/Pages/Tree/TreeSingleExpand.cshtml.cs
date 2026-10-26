using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Tree
{
    public partial class TreeSingleExpandModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnToggleExpandOptions_Click(object sender, EventArgs e)
        {
            // 只切换后续交互规则，现有节点与展开状态保留。
            bool enabled = !Tree1.EnableSingleExpand;
            Tree1.EnableSingleExpand = enabled;
            Tree1.ExpandOnDblClick = enabled;
            string status = enabled ? "开启" : "关闭";
            labExpandOptions.Text = $"同级单节点展开：{status}；双击展开：{status}";
        }



    }
}
