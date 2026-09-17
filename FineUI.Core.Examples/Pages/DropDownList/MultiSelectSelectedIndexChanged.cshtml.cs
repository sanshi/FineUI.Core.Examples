using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class MultiSelectSelectedIndexChangedModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }



        protected void btnSelectItem6_Click(object sender, EventArgs e)
        {
            DropDownList1.SelectedValue = "Value6";
        }

        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            WriteSelection();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            WriteSelection();
        }

        private void WriteSelection()
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


    }
}