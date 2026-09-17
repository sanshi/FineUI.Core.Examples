using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class AddTabRemoveOnCloseModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnAddTab3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference(new TabOptions()
            {
                ID = "dynamic_tab3",
                IFrameUrl = "https://deepseek.com/",
                Title = "DeepSeek官网（服务端代码）",
                IconUrl = IconHelper.GetIconUrl(Icon.Application),
                EnableClose = true,
                RemoveOnClose = true
            }));
        }

        protected void btnAddTab4_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetAddTabReference(new TabOptions()
            {
                ID = "dynamic_tab4",
                IFrameUrl = "https://asp.net/",
                Title = "ASP.NET官网（服务端代码）",
                IconUrl = IconHelper.GetIconUrl(Icon.ApplicationAdd),
                EnableClose = true,
                RemoveOnClose = true
            }));
        }

        protected void btnRemoveTab3_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetCloseTabReference("dynamic_tab3"));
        }

        protected void btnRemoveTab4_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(TabStrip1.GetCloseTabReference("dynamic_tab4"));
        }

    }
}