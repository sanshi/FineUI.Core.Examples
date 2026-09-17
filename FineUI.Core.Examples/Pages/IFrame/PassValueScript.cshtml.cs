using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class PassValueScriptModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("触发了 Window1 的关闭事件！");
        }

    }
}