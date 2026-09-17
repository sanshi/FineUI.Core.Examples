using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class ConfirmButtonsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "ConfirmOK")
            {
                ShowNotify("你点击了[直接退出]按钮！");
            }
            else if (e.EventName == "ConfirmCancel")
            {
                ShowNotify("你点击了[不退出]按钮！");
            }
        }


    }
}