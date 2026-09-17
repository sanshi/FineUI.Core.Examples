using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class ButtonIFrameModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("Window1 被关闭了！");
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("Window2 被关闭了！");
        }

    }
}