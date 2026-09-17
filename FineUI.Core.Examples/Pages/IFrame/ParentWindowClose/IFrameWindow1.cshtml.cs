using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.ParentWindowClose
{
    public partial class IFrameWindow1Model : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void IFrameWindow1_Window1_Close(object sender, WindowCloseEventArgs e)
        {
            // IFrameWindow1 -> labResult
            labResult.Text = DateTime.Now.ToLongTimeString();

            // 调用父页面定义的函数 updateLabelResult
            RegisterStartupScript("parent.updateLabelResult();");
        }

    }
}