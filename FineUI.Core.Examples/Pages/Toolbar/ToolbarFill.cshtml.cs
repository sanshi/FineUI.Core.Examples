using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class ToolbarFillModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnHideFill1_Click(object sender, EventArgs e)
        {
            if (Button1.Hidden)
            {
                Button1.Hidden = false;
                ToolbarFill1.Hidden = false;
            }
            else
            {
                Button1.Hidden = true;
                ToolbarFill1.Hidden = true;
            }
        }


        protected void btnHideFill2_Click(object sender, EventArgs e)
        {
            if (ToolbarFill5.Hidden)
            {
                ToolbarFill5.Hidden = false;
            }
            else
            {
                ToolbarFill5.Hidden = true;
            }
        }

    }
}