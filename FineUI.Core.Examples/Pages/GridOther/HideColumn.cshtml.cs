using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class HideColumnModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button3_Click(object sender, EventArgs e)
        {
            var scripts = String.Empty;

            var genderColumn = Grid1.FindColumn("Gender");
            if (genderColumn.Hidden)
            {
                scripts = genderColumn.GetShowReference();
            }
            else
            {
                scripts = genderColumn.GetHideReference();
            }

            RegisterStartupScript(scripts);
        }

    }
}