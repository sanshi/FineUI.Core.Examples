using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class ConfirmCancelModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Operation2")
            {
                ShowNotify("执行了操作二！");
            }
            else if (e.EventName == "Operation3_ok")
            {
                ShowNotify("执行了操作三！");
            }
            else if (e.EventName == "Operation3_cancel")
            {
                ShowNotify("取消执行操作三！");
            }
        }

        protected void btnOperation1_Click(object sender, EventArgs e)
        {
            ShowNotify("执行了操作一！");
        }

    }
}