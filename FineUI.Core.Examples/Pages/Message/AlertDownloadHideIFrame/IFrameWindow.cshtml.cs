using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message.AlertDownloadHideIFrame
{
    public partial class IFrameWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnOperation_Click(object sender, EventArgs e)
        {
            // 当前页会随窗体隐藏而关闭，确认框和下载回调必须留在打开窗体的页面执行。
            ActiveWindow.HideCallParentFunction("showConfirm");
        }

    }
}
