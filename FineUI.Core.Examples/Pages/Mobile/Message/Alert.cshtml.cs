using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.Message
{
    public partial class AlertModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "这是提示对话框的内容！";
            alert.Title = "标题文字";
            alert.MessageBoxIcon = MessageBoxIcon.Information;
            alert.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "这是提示对话框的内容！";
            alert.Title = "标题文字";
            alert.TitleAlign = TextAlign.Center;
            alert.EnableClose = false;
            alert.ButtonFill = true;
            alert.Show();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = "这是提示对话框的内容！";
            alert.Title = "标题文字";
            alert.TitleAlign = TextAlign.Center;
            alert.EnableClose = false;
            alert.ButtonPlain = true;
            alert.Show();
        }

    }
}