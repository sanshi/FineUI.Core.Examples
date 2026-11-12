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
    public partial class CheckBoxListEnableEditDefaultModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownBox1.SetUserInput("初始自定义值");
            }
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
            DropDownBox1.SetSelection(
                new[] { "php", "basic" },
                new[] { "PHP", "Basic" });
        }

        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownBox1.SetUserInput("用户输入值");
        }

        protected void btnSetCommaText_Click(object sender, EventArgs e)
        {
            // 逗号属于用户输入的文字，不能拆成两个选项。
            DropDownBox1.SetUserInput("研发, 华东");
        }

        protected void btnClearSelection_Click(object sender, EventArgs e)
        {
            DropDownBox1.ClearSelection();
        }


    }
}
