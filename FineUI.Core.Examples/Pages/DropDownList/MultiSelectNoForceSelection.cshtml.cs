using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class MultiSelectNoForceSelectionModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValueArray != null && DropDownList1.SelectedValueArray.Length > 0)
            {
                // 用户输入按文字回显，先编码动态文字，避免结果区把输入当作 HTML 标签解析。
                labResult.Text = String.Format(
                    "选中项文本：{0}<br/>选中项值：{1}",
                    HtmlEncode(DropDownList1.Text),
                    HtmlEncode(String.Join(", ", DropDownList1.SelectedValueArray)));
            }
            else
            {
                labResult.Text = String.Format("用户输入值：{0}", HtmlEncode(DropDownList1.Text));
            }

        }

        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "Value6";
        }


    }
}