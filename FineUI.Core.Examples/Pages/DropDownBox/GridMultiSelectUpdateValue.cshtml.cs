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
    public partial class GridMultiSelectUpdateValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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


        protected void btnUpdateValue_Click(object sender, EventArgs e)
        {
            // 1. 对于未分页的表格，可以将设置 Text=null 以便客户端重新计算
            // 2. 对于分页的表格，一定要手工设置 Text 属性，否则客户端无法取到 Value 对应的 Text 属性
            DropDownBox1.Values = new string[] { "102", "107", "112" };
            DropDownBox1.Text = null;
        }


    }
}