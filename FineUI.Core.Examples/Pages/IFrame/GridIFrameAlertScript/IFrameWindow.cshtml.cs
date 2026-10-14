using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.GridIFrameAlertScript
{
    public partial class IFrameWindowModel : BaseThirdPartyEditorModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateParentGrid_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 用户确认后才隐藏窗体；隐藏动画结束再调用打开窗体的页面，传递本次关闭参数。
            new Alert
            {
                Message = "保存成功！",
                MessageBoxIcon = MessageBoxIcon.Success,
                Target = Target.Top,
                OkCommand = ActiveWindow.CreateHideInvokeFunctionCommand("closeWindow1", "参数 - " + DateTime.Now.Millisecond)
            }.Show();

        }

    }
}
