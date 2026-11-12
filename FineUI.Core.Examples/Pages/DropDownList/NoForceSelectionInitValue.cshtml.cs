using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class NoForceSelectionInitValueModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                // 用户输入按文字回显，先编码动态文字，避免结果区把输入当作 HTML 标签解析。
                labResult.Text = String.Format(
                    "选中项：{0}（值：{1}）",
                    HtmlEncode(DropDownList1.Text),
                    HtmlEncode(DropDownList1.SelectedValue));
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

        protected void btnSetText_Click(object sender, EventArgs e)
        {
            DropDownList1.Text = "用户输入值";
        }
        

    }
}