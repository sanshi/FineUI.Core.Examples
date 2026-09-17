using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Panel
{
    public partial class CollapseModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Panel1_CollapseExpand(object sender, EventArgs e)
        {
            ShowNotify(String.Format("面板一处于{0}状态", Panel1.Expanded ? "展开" : "折叠"));
        }

        protected void Panel2_CollapseExpand(object sender, EventArgs e)
        {
            ShowNotify(String.Format("面板二处于{0}状态", Panel2.Expanded ? "展开" : "折叠"));
        }

    }
}