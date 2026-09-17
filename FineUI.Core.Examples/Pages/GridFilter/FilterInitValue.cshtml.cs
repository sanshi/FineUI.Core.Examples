using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridFilter
{
    public partial class FilterInitValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化列的过滤数据
                var columnName = Grid1.FindColumn("Name");
                columnName.ColumnFilteredData = new GridColumnFilteredData()
                {
                    Items = {
                        new GridColumnFilteredItem() { Value = "张" }
                    }
                };

                BindGrid();

                labResult.Text = String.Format("初始过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
            }
        }


        
        protected void Grid1_FilterChanged(object sender, EventArgs e)
        {
            BindGrid();

            labResult.Text = String.Format("过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }

        private void BindGrid()
        {
            NewFilteredTable filteredTable = new NewFilteredTable();
            filteredTable.FilterDataRowItem = FilterDataRowItemImplement;

            DataTable table = filteredTable.GetFilteredTable(Grid1);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        protected void btnUpdateFilteredData_Click(object sender, EventArgs e)
        {
            // 先清空过滤数据
            Grid1.FilteredData = null;

            // 初始化列的过滤数据
            var columnName = Grid1.FindColumn("Name");
            columnName.ColumnFilteredData = new GridColumnFilteredData()
            {
                Items = {
                    new GridColumnFilteredItem() { Value = "婷婷" }
                }
            };

            BindGrid();

            labResult.Text = String.Format("后台更新过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }


        #region FilterDataRowItem

        private bool FilterDataRowItemImplement(object sourceObj, GridColumnFilteredItem filteredItem, string columnID)
        {
            bool valid = false;

            if (columnID == "Name")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = filteredItem.Value.ToString();

                if (sourceValue.Contains(fillteredValue))
                {
                    valid = true;
                }
            }

            return valid;
        }

        #endregion

    }
}