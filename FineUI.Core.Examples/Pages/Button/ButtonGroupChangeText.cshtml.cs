using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonGroupChangeTextModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnChangeButtonText_Click(object sender, EventArgs e)
        {
            Button4.Text = Button4.Text.Length == 3 ? "按钮四（" + DateTime.Now.ToString() + "）" : "按钮四";
        }

        protected void btnShowHideButton_Click(object sender, EventArgs e)
        {
            Button4.Hidden = !Button4.Hidden;
        }


        protected void btnChangeButtonText2_Click(object sender, EventArgs e)
        {
            Button8.Text = Button8.Text.Length == 3 ? "按钮八（" + DateTime.Now.ToString() + "）" : "按钮八";
        }

        protected void btnShowHideButton2_Click(object sender, EventArgs e)
        {
            Button8.Hidden = !Button8.Hidden;
        }

    }
}