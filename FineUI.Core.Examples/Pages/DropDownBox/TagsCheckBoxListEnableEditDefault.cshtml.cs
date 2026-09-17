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
    public partial class TagsCheckBoxListEnableEditDefaultModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 设置下拉框的初始值为自定义文本
                DropDownBox1.Text = "JavaScript, 初始自定义值";
                DropDownBox1.Values = new string[] { "js", "__USERINPUT_value1" };
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
            // 后台更新下拉框的值，需要同时设置Text和Value
            DropDownBox1.Texts = new string[] { "PHP", "Basic" };
            DropDownBox1.Values = new string[] { "php", "basic" };
        }

        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownBox1.Text = "用户输入值";
            DropDownBox1.Values = new string[] { "__USERINPUT_value2" };
        }


    }
}