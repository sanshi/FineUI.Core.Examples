using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class AlertDownloadHideModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "ConfirmCancel")
            {
                ShowNotify("点击了取消按钮！");
            }
        }

        protected void btnOperation_Click(object sender, EventArgs e)
        {
            new WindowAjaxHelper(Window1).Hide();

            new Confirm
            {
                Message = "操作成功！点击确定按钮开始下载文件，点取消按钮弹出对话框",
                MessageBoxIcon = MessageBoxIcon.Question,
                OkFunction = "confirmOKCallback",
                CancelFunction = "confirmCancelCallback"
            }.Show();
        }


    }
}