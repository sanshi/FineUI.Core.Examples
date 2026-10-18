using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Window
{
    public partial class ToolbarModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }

        protected void btnChangeText_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.InvokeFunction("updateWindowLabel", "这是修改后的值！" + DateTime.Now.ToLongTimeString());
        }


    }
}