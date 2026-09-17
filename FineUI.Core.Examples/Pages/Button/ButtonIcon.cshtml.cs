using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonIconModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnCustomIcon_Click(object sender, EventArgs e)
        {
            if (btnCustomIcon.IconUrl.EndsWith("1.png"))
            {
                btnCustomIcon.IconUrl = "~/res/images/16/8.png";
            }
            else
            {
                btnCustomIcon.IconUrl = "~/res/images/16/1.png";
            }
            
        }
    }
}