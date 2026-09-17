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
    public partial class PagingSummaryDataModel : BaseModel
    {
        // GET: GridDataUrl/PagingSummaryData
        public IActionResult OnGet()
        {
            JObject result = new JObject();

            int feeTotal = 0, extraFeeTotal = 0;

            JArray ja = new JArray();
            DataTable source = DataSourceUtil.GetDataTable2();
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

                feeTotal += fee;
                extraFeeTotal += donate;
            }

            JObject joSummary = new JObject();
            joSummary.Add("Fee", feeTotal);
            joSummary.Add("ExtraFee", extraFeeTotal);

            result.Add("data", ja);
            result.Add("summaryData", joSummary);

            return Content(result.ToString(Newtonsoft.Json.Formatting.None));
        }



    }
}