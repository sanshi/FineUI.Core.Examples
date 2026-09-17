using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridBigData
{
    public partial class BigDataUrlPagingDatabaseDataModel : BaseModel
    {
        // GET: GridBigData/BigDataUrlPagingDatabaseData
        public IActionResult OnGet(int total, int pageIndex, int pageSize)
        {
            // 最大数限制
            if (total > 10000)
            {
                total = 10000;
            }

            string result = GetPagedLargeData(total, pageIndex, pageSize);

            return Content(result);
        }

        private string GetPagedLargeData(int total, int pageIndex, int pageSize)
        {
            JObject jo = new JObject();

            // 总记录数
            jo.Add("recordCount", total);

            // 分页数据
            jo.Add("data", BigDataUtil.GetBigData(total, pageIndex, pageSize));

            return jo.ToString(Newtonsoft.Json.Formatting.None);
        }

    }
}
