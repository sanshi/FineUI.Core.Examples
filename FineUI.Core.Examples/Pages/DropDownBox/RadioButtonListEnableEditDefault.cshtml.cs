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
    public partial class RadioButtonListEnableEditDefaultModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 设置下拉框的初始值为自定义文本
                DropDownBox1.Text = "初始自定义值";
            }
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownBox1.Value))
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
            DropDownBox1.Value = "js";
            DropDownBox1.Text = "JavaScript";
        }

        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownBox1.Text = "用户输入值";
            DropDownBox1.Value = null;
        }

    }
}