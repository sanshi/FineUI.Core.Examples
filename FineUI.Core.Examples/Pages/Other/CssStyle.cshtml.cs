using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class CssStyleModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            string styles = String.Format("font-size:1.5em;font-weight:bold;color:rgb({0},{1},{2});", rd.Next(256), rd.Next(256), rd.Next(256));

            Label1.CssStyle = styles;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            // 不能直接设置 CssStyle=String.Empty，客户端只会覆盖相同CSS属性的样式，而不会清空元素的 style 属性
            // 需要注册JavaScript脚本来清空元素的 style 属性
            RegisterStartupScript("F.ui.Label1.el.attr('style','');");
        }

        

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label2.CssClass = Label2.CssClass == "red" ? "green" : "red";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Label2.CssClass = "";
        }
    }
}