using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Block
{
    public partial class DashboardModel : BaseModel
    {
        public void OnGet()
        {
            var pm = PageManager.Instance;
            pm.EnableWatermark = true;
            pm.WatermarkText = "I❤︎FineUI";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        
        
    }
}