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
    public partial class TreeGridDataModel : BaseModel
    {
        // GET: GridDataUrl/TreeGridData
        public IActionResult OnGet()
        {
            JArray ja = new JArray();

            DataTable source = DataSourceUtil.GetTreeDataTable();
            foreach (DataRow row in source.Rows)
            {
                JObject jo = new JObject();
                jo.Add("ParentId", (int)row["ParentId"]);
                jo.Add("Id", (int)row["Id"]);
                jo.Add("Name", row["Name"].ToString());
                jo.Add("Type", row["Type"].ToString());
                jo.Add("ModifyDate", (DateTime)row["ModifyDate"]);

                object fileSize = row["Size"];
                if (fileSize == DBNull.Value)
                {
                    jo.Add("Size", "");
                }
                else
                {
                    jo.Add("Size", (int)fileSize);
                }

                ja.Add(jo);
            }

            return Content(ja.ToString(Newtonsoft.Json.Formatting.None));
        }




    }
}