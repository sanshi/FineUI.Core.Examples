using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Panel
{
    public partial class ToolsCustomToolAheadModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("面板处于{0}状态", Panel1.Collapsed ? "折叠" : "展开"));
        }

    }
}