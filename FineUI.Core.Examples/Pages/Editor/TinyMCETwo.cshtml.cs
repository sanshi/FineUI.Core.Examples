using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Editor
{
    public partial class TinyMCETwoModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(HtmlEditor1.Text))
            {
                ShowNotify("文章正文不能为空！");
            }
            else
            {
                ShowNotify(new RawHtml("文章标题：{0}<br/>文章正文：{1}<br/>文章摘要：{2}", HttpUtility.HtmlEncode(tbxTitle.Text), HttpUtility.HtmlEncode(HtmlEditor1.Text), HttpUtility.HtmlEncode(HtmlEditor2.Text)));
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>");
            string content = regex.Replace(HtmlEditor1.Text, "");
            if (content.Length > 100)
            {
                content = content.Substring(0, 97) + "...";
            }

            HtmlEditor2.Text = content;
        }
    }
}