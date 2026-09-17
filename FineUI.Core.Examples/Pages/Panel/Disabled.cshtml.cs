using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Panel
{
    public partial class DisabledModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }


        private void LoadData()
        {
            Panel2.Content = "可以在此放置<a href=\"http://www.w3schools.com/html/\" target=\"_blank\">HTML</a>标签。";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel1.Enabled = !Panel1.Enabled;
        }

    }
}