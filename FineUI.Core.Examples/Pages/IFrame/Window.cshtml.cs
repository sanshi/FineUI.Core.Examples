using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class WindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        //protected void btnClosePostBack_Click(object sender, EventArgs e)
        //{
        //    // 首先保存数据

        //    // 然后关闭本窗体
            
        //    RegisterStartupScript(panel1.GetClearDirtyReference() + ActiveWindow.GetHideReference());
        //}

    }
}