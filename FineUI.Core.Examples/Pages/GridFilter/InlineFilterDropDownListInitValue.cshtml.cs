using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridFilter
{
    public partial class InlineFilterDropDownListInitValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化列的过滤数据
                var columnMajor = Grid1.FindColumn("Major");
                columnMajor.ColumnFilteredData = new GridColumnFilteredData()
                {
                    Multi = false,
                    Items = {
                        new GridColumnFilteredItem() {
                            Value = new string[] { "化学系", "物理系", "数学系" }
                        }
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
            var columnMajor = Grid1.FindColumn("Major");
            columnMajor.ColumnFilteredData = new GridColumnFilteredData()
            {
                Multi = false,
                Items = {
                        new GridColumnFilteredItem() {
                            Value = new string[] { "材料科学与工程系" }
                        }
                    }
            };


            BindGrid();

            labResult.Text = String.Format("后台更新过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }


        #region FilterDataRowItem

        private bool FilterDataRowItemImplement(object sourceObj, GridColumnFilteredItem filteredItem, string columnID)
        {
            bool valid = false;

            if (columnID == "Major")
            {
                string sourceValue = sourceObj.ToString();
                string[] fillteredValue = (string[])filteredItem.Value;

                foreach (string filltereditem in fillteredValue)
                {
                    if (filltereditem == sourceValue)
                    {
                        valid = true;
                        break;
                    }
                }
            }

            return valid;
        }

        #endregion


    }
}
