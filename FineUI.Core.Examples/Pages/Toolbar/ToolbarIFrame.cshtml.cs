using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class ToolbarIFrameModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(String.Format("updateIFrameUrl('{0}');", Url.Content("~/Basic/Login")));
        }
        
    }
}