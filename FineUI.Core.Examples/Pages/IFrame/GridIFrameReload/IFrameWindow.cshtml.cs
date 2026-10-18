using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.GridIFrameReload
{
    public partial class IFrameWindowModel : ThirdPartyEditorPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateParentGrid_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 2. 不关闭窗体，直接回发父窗体
            // 窗体可能在顶层以幻影形式显示；函数仍属于最初打开窗体的页面。
            ActiveWindow.InvokeFunction("closeWindow1", "参数 - " + DateTime.Now.Millisecond);

        }

    }
}
