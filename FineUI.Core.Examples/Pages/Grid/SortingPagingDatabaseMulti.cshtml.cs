using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class SortingPagingDatabaseMultiModel : BaseModel
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
            var recordCount = DataSourceUtil.GetTotalCount();

            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = recordCount;

            // 2.获取当前分页数据
            Grid1.DataSource = DataSourceUtil.GetPagedDataTable(pageIndex: Grid1.PageIndex,
                pageSize: Grid1.PageSize,
                sortFields: Grid1.SortFieldArray);
            Grid1.DataBind();


            labSortOrderTip.Text = GetSortTip();
        }


        private string GetSortFieldTip(string sortField, string sortDirection)
        {
            var sortFieldTitle = "";
            foreach (var column in Grid1.Columns)
            {
                if (column.SortField == sortField)
                {
                    sortFieldTitle = column.HeaderText;
                    break;
                }
            }
            return String.Format("{0}（{1}）", sortFieldTitle, sortDirection == "ASC" ? "升序" : "降序");
        }

        private string GetSortTip()
        {
            List<string> sortTips = new List<string>();

            string[] sortFields = Grid1.SortFieldArray;
            // 多列排序
            if (sortFields.Length > 0)
            {
                for (var i = 0; i < sortFields.Length; i += 2)
                {
                    var sortField = sortFields[i];
                    var sortDirection = sortFields[i + 1];

                    sortTips.Add(GetSortFieldTip(sortField, sortDirection));
                }
            }

            return String.Format("排序字段：{0}", String.Join("，", sortTips));
        }


        #endregion

        protected void Grid1_Sort(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Grid1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}