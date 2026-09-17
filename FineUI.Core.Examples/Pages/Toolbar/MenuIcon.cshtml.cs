using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class MenuIconModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (MenuHyperLink1.Icon == Icon.Accept)
            {
                MenuHyperLink1.Icon = Icon.None;
            }
            else
            {
                MenuHyperLink1.Icon = Icon.Accept;
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            if (MenuHyperLink2.Icon == Icon.Accept)
            {
                MenuHyperLink2.Icon = Icon.Application;
            }
            else
            {
                MenuHyperLink2.Icon = Icon.Accept;
            }
        }

    }
}