using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.DropDownBox
{
    public partial class CheckBoxListEnableEditModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {

            if (DropDownBox1.Values != null && DropDownBox1.Values.Length > 0)
            {
                // 用户输入按文字回显，先编码动态文字，避免结果区把输入当作 HTML 标签解析。
                labResult.Text = String.Format(
                    "下拉框文本：{0}（值：{1}）",
                    HtmlEncode(DropDownBox1.Text),
                    HtmlEncode(String.Join(", ", DropDownBox1.Values)));
            }
            else
            {
                labResult.Text = String.Format("用户输入值：{0}", HtmlEncode(DropDownBox1.Text));
            }
        }


        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            // 后台更新下拉框的值，需要同时设置Text和Value
            DropDownBox1.Texts = new string[] { "PHP", "Basic" };
            DropDownBox1.Values = new string[] { "php", "basic" };
        }

    }
}