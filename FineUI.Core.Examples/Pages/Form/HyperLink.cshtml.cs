using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class HyperLinkModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }


        
        protected void btnChangeEnable_Click(object sender, EventArgs e)
        {
            HyperLink2.Enabled = !HyperLink2.Enabled;
        }

    }
}