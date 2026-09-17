using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridTree
{
    public partial class PagingModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }



        #region BindGrid

        private void LoadData()
        {
            var recordCount = GetTotalCount();

            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = recordCount;

            // 2.获取当前分页数据
            Grid1.DataSource = GetPagedDataTable(pageIndex: Grid1.PageIndex, pageSize: Grid1.PageSize);
            Grid1.DataBind();
        }


        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            DataTable source = DataSourceUtil.GetTreeDataTable();

            return GetTopNodeTable(source).Rows.Count;
        }

        private DataTable GetTopNodeTable(DataTable source)
        {
            DataTable topTable = source.Clone();

            foreach (DataRow row in source.Rows)
            {
                int parentId = Convert.ToInt32(row["ParentId"]);

                if (parentId == -1)
                {
                    topTable.ImportRow(row);
                }
            }

            return topTable;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize)
        {
            DataTable source = DataSourceUtil.GetTreeDataTable();
            DataTable topTable = GetTopNodeTable(source);

            int recordCount = topTable.Rows.Count;

            DataTable paged = source.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > recordCount)
            {
                rowend = recordCount;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                DataRow topTableRow = topTable.Rows[i];
                paged.ImportRow(topTableRow);

                ImportCurrentPageTable(source, paged, Convert.ToInt32(topTableRow["Id"]));
            }

            return paged;
        }

        // 递归查找需要的行
        private void ImportCurrentPageTable(DataTable source, DataTable paged, int rowId)
        {
            foreach (DataRow row in source.Rows)
            {
                int parentRowId = Convert.ToInt32(row["ParentId"]);

                if (rowId == parentRowId)
                {
                    paged.ImportRow(row);

                    ImportCurrentPageTable(source, paged, Convert.ToInt32(row["Id"]));
                }
            }
        }

        #endregion


        protected void Grid1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}