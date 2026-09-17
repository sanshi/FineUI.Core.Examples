using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class PassValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string openUrl = String.Format("{0}?selected={1}", Url.Content("~/IFrame/PassValue/IFrameWindow"), HttpUtility.UrlEncode(tbxProvince.Text));

            RegisterStartupScript(Window1.GetSaveStateReference("tbxProvince") + Window1.GetShowReference(openUrl));
        }


        //protected void Window1_Close(object sender, WindowCloseEventArgs e)
        //{
        //    ShowNotify("触发了 Window1 的关闭事件！");
        //}



    }
}