using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class PageItemsRowExpanderModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnShowRowExpanders_Click(object sender, EventArgs e)
        {
            Grid1.ExpandAllRowExpanders = !Grid1.ExpandAllRowExpanders;
        }

    }
}