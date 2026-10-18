using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class JSErrorModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button4_Click(object sender, EventArgs e)
        {
            global::FineUI.Core.PageContext.InvokeFunction("test");
        }

    }
}