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
    public partial class TagsCheckBoxListEnableEditDefaultModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 一个真实选项和一个自定义标签按位置配对，初始自定义值不会误认作列表项。
                DropDownBox1.SetSelection(
                    new[] { "js", "__USERINPUT_value1" },
                    new[] { "JavaScript", "初始自定义值" });
            }
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownBox1.Text))
            {
                labResult.Text = String.Format("下拉框文本：{0}（值：{1}）", DropDownBox1.Text, String.Join(", ", DropDownBox1.Values));
            }
            else
            {
                labResult.Text = "下拉框为空";
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
            DropDownBox1.SetUserInput("研发, 华东");
        }

        protected void btnClearSelection_Click(object sender, EventArgs e)
        {
            DropDownBox1.ClearSelection();
        }


    }
}
