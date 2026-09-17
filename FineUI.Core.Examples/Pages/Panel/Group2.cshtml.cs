using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Panel
{
    public partial class Group2Model : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button2_Click(object sender, EventArgs e)
        {
            GroupPanel2.Collapsed = !GroupPanel2.Collapsed;
        }

    }
}