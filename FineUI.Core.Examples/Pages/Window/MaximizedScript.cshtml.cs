using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Window
{
    public partial class MaximizedScriptModel : BaseModel
    {
        protected void btnServerMaximize_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
        }

        protected void btnServerRestore_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.RegisterStartupScript(Window1.GetRestoreReference());
        }

        protected void btnServerClose_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.RegisterStartupScript(Window1.GetCloseReference());
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

    }
}
