using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.CSP
{
    public partial class GridModel : BaseModel
    {
        public void OnGet()
        {
            var pm = PageManager.Instance;
            pm.CspScripts = true;
            pm.CspScriptsAllowNonce = true;
            pm.CspScriptsAllowUrls = new[] { "cdn.jsdelivr.net", "unpkg.com" };
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        


    }
}