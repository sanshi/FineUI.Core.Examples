using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TriggerBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void btnCloseWindow_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
            TriggerBox1.Text = "弹出窗口被关闭了";
        }

    }
}