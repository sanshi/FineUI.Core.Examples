using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class AddTabModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnAddTab3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference("dynamic_tab3", "https://deepseek.com/", "DeepSeek官网（服务端代码）", IconHelper.GetIconUrl(Icon.Application), true));
        }

        protected void btnAddTab4_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference("dynamic_tab4", "https://asp.net/", "ASP.NET官网（服务端代码）", IconHelper.GetIconUrl(Icon.ApplicationAdd), true));
        }

        protected void btnRemoveTab3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetHideTabReference("dynamic_tab3"));
        }

        protected void btnRemoveTab4_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetHideTabReference("dynamic_tab4"));
        }

    }
}