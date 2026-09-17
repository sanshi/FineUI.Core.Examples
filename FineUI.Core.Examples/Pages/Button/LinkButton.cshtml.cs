using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class LinkButtonModel : BaseModel
    {
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            ShowNotify("这是服务器端事件");
        }

        protected void btnChangeEnable_Click(object sender, EventArgs e)
        {
            LinkButton1.Enabled = !LinkButton1.Enabled;
        }

    }
}