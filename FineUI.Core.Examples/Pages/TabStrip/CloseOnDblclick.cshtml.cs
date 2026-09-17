using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class CloseOnDblclickModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnShowInServer_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Tab3.GetShowReference());
        }

        protected void btnShowActiveInServer_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Tab3.GetActivateReference());
        }


        protected void btnHideInServer_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Tab3.GetHideReference());
        }


    }
}