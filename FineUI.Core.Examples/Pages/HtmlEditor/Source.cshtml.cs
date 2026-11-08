using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.HtmlEditor
{
    public partial class SourceModel : PageBase
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            TextArea1.Text = HtmlEditor1.Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HtmlEditor1.Text = TextArea1.Text;
        }
    }
}
