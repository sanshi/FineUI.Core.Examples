using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Editor
{
    public partial class TinyMCEWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(HtmlEditor1.Text))
            {
                ShowNotify("编辑器内容为空！");
            }
            else
            {
                ShowNotify(HtmlEditor1.Text);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HtmlEditor1.Text = "<p><strong>FineUI.Core</strong> - .NET 企业级全栈 UI 框架。</p>";
        }

    }
}