using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class AlertCustomIconModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnHello_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "你好 FineUI.Core！";
            alert.Icon = Icon.Book;
            alert.Show();
        }

        protected void btnHello2_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "你好 FineUI.Core！";
            alert.IconUrl = "~/res/images/success.png";
            alert.Target = Target.Top;
            alert.Show();
        }

        protected void btnHello3_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "你好 FineUI.Core！";
            alert.IconFont = IconFont._Car;
            alert.Target = Target.Top;
            alert.Show();
        }

    }
}