using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridDataUrl
{
    public partial class PagingDatabaseSummaryDataModel : BaseModel
    {
        // GET: GridDataUrl/PagingDatabaseSummaryData
        public IActionResult OnGet(int pageIndex, int pageSize)
        {
            JObject result = new JObject();
            // 总记录数
            result.Add("recordCount", GetTotalCount());


            // 当前分页数据
            JArray ja = new JArray();
            DataTable source = GetPagedDataTable(pageIndex, pageSize);
            foreach (DataRow row in source.Rows)
            {
                int fee = (int)row["Fee"];
                int donate = (int)row["ExtraFee"];

                JObject jo = new JObject();
                jo.Add("Id", (int)row["Id"]);
                jo.Add("Name", row["Name"].ToString());
                jo.Add("Gender", (int)row["Gender"]);
                jo.Add("EntranceYear", (int)row["EntranceYear"]);
                jo.Add("AtSchool", (bool)row["AtSchool"]);
                jo.Add("Major", row["Major"].ToString());
                jo.Add("Fee", fee);
                jo.Add("ExtraFee", donate);

                ja.Add(jo);
            }


            result.Add("data", ja);

            // 只在请求第一页数据时计算合计行数据
            if (pageIndex == 0)
            {
                result.Add("summaryData", GetSummaryData());
            }

            return Content(result.ToString(Newtonsoft.Json.Formatting.None));
        }

        private JObject GetSummaryData()
        {
            DataTable source = DataSourceUtil.GetDataTable2();

            int extraFeeTotal = 0;
            int feeTotal = 0;
            foreach (DataRow row in source.Rows)
            {
                extraFeeTotal += Convert.ToInt32(row["ExtraFee"]);
                feeTotal += Convert.ToInt32(row["Fee"]);
            }


            JObject summary = new JObject();
            summary.Add("Fee", feeTotal);
            summary.Add("ExtraFee", extraFeeTotal);

            return summary;
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            return DataSourceUtil.GetDataTable2().Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize)
        {
            DataTable source = DataSourceUtil.GetDataTable2();

            DataTable paged = source.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > source.Rows.Count)
            {
                rowend = source.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(source.Rows[i]);
            }

            return paged;
        }





    }
}