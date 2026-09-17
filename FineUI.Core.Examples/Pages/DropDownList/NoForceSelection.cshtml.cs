using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class NoForceSelectionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }


        
        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                labResult.Text = String.Format("选中项：{0}（值：{1}）", DropDownList1.Text, DropDownList1.SelectedValue);
            }
            else
            {
                labResult.Text = String.IsNullOrEmpty(DropDownList1.Text) ? "无选中项" : String.Format("用户输入值：{0}", DropDownList1.Text);
            }
        }

        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "Value6";
        }

    }
}