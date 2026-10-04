using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridDataUrl
{
    // 独立 HTTP 响应入口：不渲染 FineUI 页面，也不初始化页面主题等配置。
    public class FilterDataModel : PageModel
    {
        // 本入口只接收 POST；GET 不应返回空白页面。
        public IActionResult OnGet()
        {
            Response.Headers["Allow"] = "POST";
            return StatusCode(405);
        }

        public IActionResult OnPost(JArray filteredData)
        {
            FilteredTable filteredTable = new FilteredTable();
            filteredTable.FilterDataRowItem = FilterDataRowItemImplement;

            DataTable source = filteredTable.GetFilteredTable(filteredData);

            JArray ja = new JArray();
            foreach (DataRow row in source.Rows)
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

        private bool FilterDataRowItemImplement(object sourceObj, string fillteredOperator, JToken fillteredObj, string column)
        {
            bool valid = false;

            if (column == "Name")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = fillteredObj.Value<string>();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "start")
                {
                    if (sourceValue.StartsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "end")
                {
                    if (sourceValue.EndsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }
            else if (column == "EntranceYear")
            {
                int sourceValue = Convert.ToInt32(sourceObj);
                int fillteredValue = fillteredObj.Value<int>();

                if (fillteredOperator == "greater")
                {
                    if (sourceValue > fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "less")
                {
                    if (sourceValue < fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }

            }
            else if (column == "Major")
            {
                string sourceValue = sourceObj.ToString();
                JArray fillteredValue = (JArray)fillteredObj;

                foreach (string filltereditem in fillteredValue)
                {
                    if (filltereditem == sourceValue)
                    {
                        valid = true;
                        break;
                    }
                }
            }

            return valid;
        }
    }
}
