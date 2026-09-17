using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class TabIndexChangedModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button3_Click(object sender, EventArgs e)
        {
            int nextIndex = TabStrip1.ActiveTabIndex + 1;

            if (nextIndex >= 3)
            {
                nextIndex = 0;
            }

            TabStrip1.ActiveTabIndex = nextIndex;
        }


        protected void TabStrip1_TabIndexChanged(object sender, EventArgs e)
        {
            if (TabStrip1.ActiveTabIndex == 0)
            {
                Label1.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
            else if (TabStrip1.ActiveTabIndex == 1)
            {
                Label2.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
            else if (TabStrip1.ActiveTabIndex == 2)
            {
                Label3.Text = "标签回发时间：" + DateTime.Now.ToLongTimeString();
            }
        }
        
    }
}