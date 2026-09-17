using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Accordion
{
    public partial class AccordionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("当前展开的是第 {0} 个面板", Accordion1.ActivePaneIndex + 1));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int nextIndex = Accordion1.ActivePaneIndex + 1;

            if (nextIndex >= 3)
            {
                nextIndex = 0;
            }

            Accordion1.ActivePaneIndex = nextIndex;
        }
    }
}