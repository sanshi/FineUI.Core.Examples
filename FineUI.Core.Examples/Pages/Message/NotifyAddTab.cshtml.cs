using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class NotifyAddTabModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnOperation1_Click(object sender, EventArgs e)
        {
            // 随机生成通知对话框的客户端ID
            string notifyId = Guid.NewGuid().ToString();

            Notify n = new Notify();
            n.ID = notifyId;
            // 通知 ID 是本次生成的 GUID；链接仅携带数据，动作由页面的委托事件处理。
            n.MessageRawHtml = new RawHtml("<div class=\"addtabcontainer\"><a class=\"addtablink\" href=\"#\" data-notify-id=\"" + notifyId + "\">向父页面添加选项卡</a></div>");
            n.MessageBoxIcon = MessageBoxIcon.None;
            n.PositionX = Position.Right;
            n.PositionY = Position.Bottom;
            n.DisplayMilliseconds = 0;
            n.ShowHeader = false;

            n.Show();
        }

    }
}
