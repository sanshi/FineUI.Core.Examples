using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.IFrameCloseAll
{
    public partial class IFrameWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            labResult.Text = "Window3 关闭了，时间：" + DateTime.Now.ToLongTimeString();

            ActiveWindow.HidePostBack();
        }

        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {
            labResult.Text = "Window4 关闭了，时间：" + DateTime.Now.ToLongTimeString();

            ActiveWindow.HidePostBack();
        }
    }
}