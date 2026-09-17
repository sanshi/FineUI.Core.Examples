using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.DropDownList
{
    public partial class ItemDataBoundDisplayModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDataTableToDropDownList();
            }
        }

        #region BindDataTableToDropDownList

        private void BindDataTableToDropDownList()
        {
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataSource = DataSourceUtil.GetDataTable();
            DropDownList1.DataBind();
        }

        #endregion

        #region Events

        protected void DropDownList1_ItemDataBound(object sender, ListItemEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;

            e.Item.Display = String.Format("<div class=\"item-text\">{0}（{2}）</div><div class=\"item-desc\">{1}</div>", row["Name"], row["Desc"], row["Id"]);
        }



        protected void btnGetSelection_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DropDownList1.Text))
            {
                labResult.Text = String.Format("选中项：{0}（值：{1}）", DropDownList1.Text, DropDownList1.SelectedValue);
            }
            else
            {
                labResult.Text = "无选中项";
            }
        }

        #endregion

    }
}