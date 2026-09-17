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
    public partial class BigDataUrlDataModel : BaseModel
    {
        // GET: GridBigData/BigDataUrlData
        public IActionResult OnGet(int total, string resultType)
        {
            string result = String.Empty;

            // 最大数限制
            if (total > 10000)
            {
                total = 10000;
            }

            if (resultType == "simple")
            {
                // 数据格式一
                result = BigDataUtil.GetSimpleBigDataString(total);
            }
            else
            {
                // 数据格式二
                result = BigDataUtil.GetBigDataString(total);
            }

            return Content(result);
        }

    }
}
