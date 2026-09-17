using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class SummaryScrollbarBottomModel : BaseModel
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
            var pagedDataTable = DataSourceUtil.GetPagedDataTable(pageIndex: Grid1.PageIndex, pageSize: Grid1.PageSize);
            Grid1.DataSource = pagedDataTable;
            Grid1.DataBind();

            // 3. 合计行数据
            Grid1.SummaryData = GetSummaryData(pagedDataTable);
        }

        private JObject GetSummaryData(DataTable source)
        {
            float extraFeeTotal = 0.0f;
            float feeTotal = 0.0f;
            foreach (DataRow row in source.Rows)
            {
                extraFeeTotal += Convert.ToInt32(row["ExtraFee"]);
                feeTotal += Convert.ToInt32(row["Fee"]);
            }


            JObject summary = new JObject();
            //summary.Add("Major", "全部合计");
            summary.Add("Fee", feeTotal.ToString("F2"));
            summary.Add("ExtraFee", extraFeeTotal.ToString("F2"));

            return summary;
        }


        #endregion

        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            LoadData();
        }


    }
}
