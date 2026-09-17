using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class CustomPostbackJsonModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public IActionResult OnPostTextBox1_ENTER(string text1)
        {
            return new JsonResult(new { type = "enter", text = text1 + " - server" });
        }

    }
}