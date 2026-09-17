using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class AuthenticationTimeoutModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect(Url.Content("~/?ReturnUrl=%2fOther%2fAuthenticationTimeout"));
        }

    }
}