using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class LabelModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }



        protected void btnChangeEnable_Click(object sender, EventArgs e)
        {
            Label3.Enabled = !Label3.Enabled;

        }

    }
}