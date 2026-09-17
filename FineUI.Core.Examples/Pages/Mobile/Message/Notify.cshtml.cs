using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.Message
{
    public partial class NotifyModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Notify notify = new Notify();
            notify.Message = "数据保存成功！";
            notify.Title = "通知";
            notify.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Notify notify = new Notify();
            notify.Message = "正在加载...";
            notify.MessageBoxIcon = MessageBoxIcon.None;
            notify.ShowHeader = false;
            notify.ShowLoading = true;
            notify.PositionX = Position.Center;
            notify.PositionY = Position.Center;
            notify.MinWidth = 0;
            notify.IsModal = true;
            notify.HideOnMaskClick = true;
            notify.Show();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Notify notify = new Notify();
            notify.CssClass = "mynotify-notext";
            notify.MessageBoxIcon = MessageBoxIcon.None;
            notify.ShowHeader = false;
            notify.ShowLoading = true;
            notify.PositionX = Position.Center;
            notify.PositionY = Position.Center;
            notify.MinWidth = 0;
            notify.IsModal = true;
            notify.HideOnMaskClick = true;
            notify.DisplayMilliseconds = 1000000;
            notify.Show();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Notify notify = new Notify();
            notify.CssClass = "mynotify";
            notify.MessageRawHtml = new RawHtml("<div class=\"f-loading\"><div class=\"f-loading-img\"><img src=\"{0}\"/></div></div><div class=\"f-loading-message\">正在加载</div>",
                FineUI.Core.PageContext.ResolveUrl("~/res/images/loading/loading_32.gif"));
            notify.MessageBoxIcon = MessageBoxIcon.None;
            notify.ShowHeader = false;
            notify.PositionX = Position.Center;
            notify.PositionY = Position.Center;
            notify.MinWidth = 0;
            notify.IsModal = true;
            notify.HideOnMaskClick = true;
            notify.DisplayMilliseconds = 1000000;
            notify.Show();
        }

    }
}