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
    public partial class RadioButtonListEnableEditDefaultModel : PageBase
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
            // 空字符串也可以是选中项的 ID；用选中项数量区分选择与自由文字。
            if (DropDownBox1.Values.Length > 0)
            {
                labResult.Text = String.Format("下拉框文本：{0}（值：{1}）", DropDownBox1.Text, DropDownBox1.Value);
            }
            else
            {
                labResult.Text = String.Format("用户输入值：{0}", DropDownBox1.Text);
            }
        }


        protected void btnSelectItem_Click(object sender, EventArgs e)
        {
            DropDownBox1.SetSelection(new[] { "js" }, new[] { "JavaScript" });
        }

        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownBox1.SetUserInput("用户输入值");
        }

    }
}
