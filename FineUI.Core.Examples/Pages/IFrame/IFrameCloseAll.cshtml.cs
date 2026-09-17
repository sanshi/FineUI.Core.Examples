using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class IFrameCloseAllModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            labResult.Text = "Window1 关闭了，时间：" + DateTime.Now.ToLongTimeString();
        }

        protected void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            labResult.Text = "Window2 关闭了，时间：" + DateTime.Now.ToLongTimeString();
        }


        


    }
}