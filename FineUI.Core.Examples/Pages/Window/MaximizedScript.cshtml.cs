using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Window
{
    public partial class MaximizedScriptModel : BaseModel
    {
        protected void btnServerMaximize_Click(object sender, EventArgs e)
        {
            new WindowAjaxHelper(Window1).Maximize();
        }

        protected void btnServerRestore_Click(object sender, EventArgs e)
        {
            new WindowAjaxHelper(Window1).Restore();
        }

        protected void btnServerClose_Click(object sender, EventArgs e)
        {
            new WindowAjaxHelper(Window1).Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }




    }
}
