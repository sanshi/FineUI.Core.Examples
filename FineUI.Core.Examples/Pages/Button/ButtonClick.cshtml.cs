using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonClickModel : BaseModel
    {
        protected void btnServerClick_Click(object sender, EventArgs e)
        {
            ShowNotify("这是服务器端事件");
        }

        protected void btnChangeClientClick2_Click(object sender, EventArgs e)
        {
            // 回发中换掉客户端回调：下发的也只是新函数名，不是脚本
            btnClientClick2.ClickHandler = "onChangedClick";
        }


    }
}