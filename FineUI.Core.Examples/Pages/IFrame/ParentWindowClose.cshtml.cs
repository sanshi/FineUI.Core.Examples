using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class ParentWindowCloseModel : BaseModel
    {
        protected void btnServerRefresh_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.RegisterStartupScript(Panel1.GetRefreshIFrameReference());
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        


    }
}
