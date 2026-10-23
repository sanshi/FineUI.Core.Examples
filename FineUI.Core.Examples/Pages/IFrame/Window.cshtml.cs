using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class WindowModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void btnUpdateIFrameTitle_Click(object sender, EventArgs e)
        {
            // 只改标题；sandbox 和 referrerpolicy 仍沿用当前属性表中的值。
            Window1.IFrameAttributes["title"] = "服务端已更新标题";
        }

        protected void btnRemoveIFrameReferrerPolicy_Click(object sender, EventArgs e)
        {
            // 移除来源策略后，标题和沙箱约束仍按当前值保留。
            Window1.RemoveIFrameAttribute("referrerpolicy");
        }


    }
}
