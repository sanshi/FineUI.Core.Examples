using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class FieldLabelStyleModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSwitchClass_Click(object sender, EventArgs e)
        {
            if (tbxUserName.CssClass != "red")
            {
                tbxUserName.CssClass = "red";
                tbxPassword.CssClass = "red";
            }
            else
            {
                tbxUserName.CssClass = "blue";
                tbxPassword.CssClass = "blue";
            }
        }

    }
}