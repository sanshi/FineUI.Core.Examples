using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonCustomModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        
        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify("点击了普通按钮");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ShowNotify("点击了自定义按钮");
        }

    }
}