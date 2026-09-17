using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class MultiSelectTagsNoForceSelectionInitValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList1.Text = "可选项1, 可选项4, 初始自定义值1";
                DropDownList1.SelectedValueArray = new string[] { "Value1", "Value4", "__USERINPUT_value1" };
            }
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.Text))
            {
                labResult.Text = String.Format("选中项文本：{0}<br/>选中项值：{1}", DropDownList1.Text, String.Join(", ", DropDownList1.SelectedValueArray));
            }
            else
            {
                labResult.Text = "无选中项";
            }

        }

        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "Value6";
        }


    }
}