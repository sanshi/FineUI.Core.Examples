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
    public partial class GridLayoutAlwaysDisplayModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        #region BindGrid

        private void BindGrid()
        {
            // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
            Grid1.RecordCount = GetTotalCount();

            // 2.获取当前分页数据
            DataTable table = GetPagedDataTable();

            // 3.绑定到Grid
            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            return GetSource().Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable()
        {
            int pageIndex = Grid1.PageIndex;
            int pageSize = Grid1.PageSize;

            DataTable table = GetSource();

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

        private DataTable GetSource()
        {
            string sortField = Grid1.SortField;
            string sortDirection = Grid1.SortDirection;

            DataTable table2 = DataSourceUtil.GetDataTable2();

            DataView view2 = table2.DefaultView;
            view2.Sort = String.Format("{0} {1}", sortField, sortDirection);

            List<string> filters = new List<string>();

            string searchKeyword = ttbSearch.Text.Trim();
            if (!String.IsNullOrEmpty(searchKeyword) && ttbSearch.ShowTrigger1)
            {
                // RowFilter的用法：http://www.csharp-examples.net/dataview-rowfilter/
                filters.Add(String.Format("Name LIKE '*{0}*'", DataSourceUtil.EscapeLikeValue(searchKeyword)));
            }


            if (!String.IsNullOrEmpty(rblAtSchool.SelectedValue) && rblAtSchool.SelectedValue != "-1")
            {
                filters.Add(String.Format("AtSchool = {0}", rblAtSchool.SelectedValue));
            }

            if (filters.Count > 0)
            {
                view2.RowFilter = String.Join(" AND ", filters.ToArray());
            }

            return view2.ToTable();
        }


        #endregion

        #region Events



        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            BindGrid();
        }


        protected void ttbSearch_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearch.Text = String.Empty;
            ttbSearch.ShowTrigger1 = false;

            BindGrid();
        }

        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearch.ShowTrigger1 = true;

            BindGrid();
        }

        protected void rblAtSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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


        #endregion




    }
}