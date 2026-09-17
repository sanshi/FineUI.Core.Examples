using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;


namespace FineUI.Core.Examples.Pages.DropDownBox
{
    public partial class GridSearchDropDownListModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }


        }


        private void LoadData()
        {
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = GetTotalCount(ddlAtSchool.SelectedValue, Grid1.FilteredData);

            // 2.获取当前分页数据
            Grid1.DataSource = GetPagedDataTable(pageIndex: Grid1.PageIndex, pageSize: Grid1.PageSize,
                ddlAtSchool: ddlAtSchool.SelectedValue, filteredData: Grid1.FilteredData);
            Grid1.DataBind();

        }

        protected void ddlAtSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            LoadData();
        }

        protected void Grid1_FilterChanged(object sender, EventArgs e)
        {
            LoadData();
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

        #region Data

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            return GetTotalCount("-1", null);
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount(string ddlAtSchool, JArray filteredData)
        {
            return GetSource(ddlAtSchool, filteredData).Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize)
        {
            return GetPagedDataTable(pageIndex, pageSize, "-1", null);
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize, string ddlAtSchool, JArray filteredData)
        {
            DataTable table = GetSource(ddlAtSchool, filteredData);

            int recordCount = table.Rows.Count;
            pageIndex = DataSourceUtil.ValidatePageIndex(pageIndex, pageSize, recordCount);

            DataTable paged = table.Clone();
            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > table.Rows.Count)
            {
                rowend = table.Rows.Count;
            }
            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(table.Rows[i]);
            }

            return paged;
        }

        private DataTable GetSource(string ddlAtSchool, JArray filteredData)
        {
            DataTable table2 = DataSourceUtil.GetDataTable2();

            DataView view2 = table2.DefaultView;

            List<string> filters = new List<string>();

            // 表格工具栏中的下拉列表过滤项
            if (!String.IsNullOrEmpty(ddlAtSchool) && ddlAtSchool != "-1")
            {
                filters.Add(String.Format("AtSchool = {0}", ddlAtSchool));
            }

            // 表头菜单的过滤项
            if (filteredData != null && filteredData.Count > 0)
            {
                foreach (JObject filteredObj in filteredData)
                {
                    // 本过滤项是[所学专业]列的过滤项
                    string columnID = filteredObj.Value<string>("column");
                    JArray items = filteredObj.Value<JArray>("items");

                    if (columnID == "Major")
                    {
                        JObject item = items.First as JObject;
                        string itemOperator = item.Value<string>("operator");
                        string itemValue = item.Value<string>("value");

                        if (!String.IsNullOrEmpty(itemValue))
                        {
                            string escapedValue = DataSourceUtil.EscapeLikeValue(itemValue);
                            if (itemOperator == "equal")
                            {
                                filters.Add(String.Format("Major = '{0}'", escapedValue));
                            }
                            else if (itemOperator == "contain")
                            {
                                filters.Add(String.Format("Major LIKE '*{0}*'", escapedValue));
                            }
                            else if (itemOperator == "start")
                            {
                                filters.Add(String.Format("Major LIKE '{0}*'", escapedValue));
                            }
                            else if (itemOperator == "end")
                            {
                                filters.Add(String.Format("Major LIKE '*{0}'", escapedValue));
                            }
                        }
                    }
                }
            }

            if (filters.Count > 0)
            {
                // RowFilter的用法：http://www.csharp-examples.net/dataview-rowfilter/
                view2.RowFilter = String.Join(" AND ", filters.ToArray());
            }

            return view2.ToTable();
        }

        #endregion
    }
}
