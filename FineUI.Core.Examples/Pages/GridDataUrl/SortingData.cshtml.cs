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
    public partial class SortingDataModel : BaseModel
    {
        // GET: GridDataUrl/SortingData
        public IActionResult OnGet(string sortField, string sortDirection, bool? data2)
        {
            JArray ja = new JArray();

            DataTable source = null;
            if (data2.HasValue && data2.Value)
            {
                source = DataSourceUtil.GetDataTable2();
            }
            else
            {
                source = DataSourceUtil.GetDataTable();
            }

            DataView view1 = source.DefaultView;
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection);

            DataTable sortedTable = view1.ToTable();

            foreach (DataRow row in sortedTable.Rows)
            {
                JObject jo = new JObject();
                jo.Add("Id", (int)row["Id"]);
                jo.Add("Name", row["Name"].ToString());
                jo.Add("Gender", (int)row["Gender"]);
                jo.Add("EntranceYear", (int)row["EntranceYear"]);
                jo.Add("AtSchool", (bool)row["AtSchool"]);
                jo.Add("Major", row["Major"].ToString());
                jo.Add("Group", (int)row["Group"]);

                ja.Add(jo);
            }

            return Content(ja.ToString(Newtonsoft.Json.Formatting.None));
        }




    }
}