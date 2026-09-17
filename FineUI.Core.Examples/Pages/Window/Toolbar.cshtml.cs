using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Window
{
    public partial class ToolbarModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }

        protected void btnChangeText_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(String.Format("$('#mylabel').html('{0}')", "这是修改后的值！" + DateTime.Now.ToLongTimeString()));
        }

        
    }
}