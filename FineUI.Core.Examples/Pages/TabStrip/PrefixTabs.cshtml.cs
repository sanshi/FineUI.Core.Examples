using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class PrefixTabsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference("tab1_iframe", "https://deepseek.com/", "DeepSeek官网", true));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference("tab1_iframe", "https://asp.net/", "ASP.NET官网", true));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference("tab2_iframe", "https://fineui.com/", "FineUI官网", true));
        }

    }
}