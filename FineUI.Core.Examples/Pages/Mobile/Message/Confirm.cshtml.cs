using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.Message
{
    public partial class ConfirmModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Button4_ConfirmResult")
            {
                ShowNotify(String.Format("你点击了 Button4 对话框的 {0} 按钮", e.EventArguments));
            }
            else if (e.EventName == "Button5_ConfirmResult")
            {
                ShowNotify(String.Format("你点击了 Button5 对话框的 {0} 按钮", e.EventArguments));
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.Message = "您真的要执行删除操作吗？";
            confirm.Title = "确认操作";
            confirm.MessageBoxIcon = MessageBoxIcon.Question;
            confirm.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.Message = "您真的要执行删除操作吗？";
            confirm.Title = "确认操作";
            confirm.TitleAlign = TextAlign.Center;
            confirm.EnableClose = false;
            confirm.ButtonFill = true;
            confirm.Show();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.Message = "您真的要执行删除操作吗？";
            confirm.Title = "确认操作";
            confirm.TitleAlign = TextAlign.Center;
            confirm.EnableClose = false;
            confirm.ButtonPlain = true;
            confirm.CancelButtonAhead = true;
            confirm.Show();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Confirm confirm = new Confirm();
            confirm.Message = "您真的要执行删除操作吗？";
            confirm.Title = "确认操作";
            confirm.TitleAlign = TextAlign.Center;
            confirm.EnableClose = false;
            confirm.ButtonPlain = true;
            confirm.Show();
        }


    }
}