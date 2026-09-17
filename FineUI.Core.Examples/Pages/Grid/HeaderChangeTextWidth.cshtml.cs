using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class HeaderChangeTextWidthModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(String.Format("changeColumnTitleAndWidth('入学年份（已修改）', 200);"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(String.Format("changeColumnTitleAndWidth('入学年份', 120);"));
        }

    }
}