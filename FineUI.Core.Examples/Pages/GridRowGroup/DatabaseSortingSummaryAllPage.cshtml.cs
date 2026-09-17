using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridRowGroup
{
    public partial class DatabaseSortingSummaryAllPageModel : BaseModel
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
            var pagedDataTable = DataSourceUtil.GetPagedDataTable(pageIndex: Grid1.PageIndex,
                pageSize: Grid1.PageSize,
                sortField: Grid1.SortField,
                sortDirection: Grid1.SortDirection);
            Grid1.DataSource = pagedDataTable;
            Grid1.DataBind();

            // 3. 合计行数据
            Grid1.SummaryDataArray = GetSummaryDataArray(pagedDataTable);
        }



        private JArray GetSummaryDataArray(DataTable source)
        {
            JArray summaryArray = new JArray();

            // 分页合计
            summaryArray.Add(CalcSummaryRow(source, "平均（当前页）："));

            // 全部合计
            summaryArray.Add(CalcSummaryRow(DataSourceUtil.GetDataTable2(), "平均（全部页）："));

            return summaryArray;
        }

        private JObject CalcSummaryRow(DataTable source, string title)
        {
            int chineseScoreTotal = 0;
            int mathScoreTotal = 0;
            int rowCount = source.Rows.Count;
            foreach (DataRow row in source.Rows)
            {
                chineseScoreTotal += Convert.ToInt32(row["ChineseScore"]);
                mathScoreTotal += Convert.ToInt32(row["MathScore"]);
            }


            JObject summary = new JObject();
            summary.Add("Major", title);
            summary.Add("ChineseScore", (chineseScoreTotal / rowCount).ToString("F2"));
            summary.Add("MathScore", (mathScoreTotal / rowCount).ToString("F2"));

            return summary;
        }



        #endregion

        protected void Grid1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void Grid1_Sort(object sender, EventArgs e)
        {
            LoadData();
        }



    }
}