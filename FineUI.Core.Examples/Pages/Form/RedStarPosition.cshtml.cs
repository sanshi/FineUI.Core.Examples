using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class RedStarPositionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm1);
        }

        protected void btnLogin2_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm2);
        }

        protected void btnLogin3_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm3);
        }

    }
}