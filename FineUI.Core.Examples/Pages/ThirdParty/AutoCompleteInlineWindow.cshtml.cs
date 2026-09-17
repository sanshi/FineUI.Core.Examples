using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class AutoCompleteInlineWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("用户输入值：{0}", TextBox1.Text));
        }

    }
}