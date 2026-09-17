using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class TabStripModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify("你点击了位于第二个标签中的一个按钮！");
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

    }
}