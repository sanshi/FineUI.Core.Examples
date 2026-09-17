using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web;


namespace FineUI.Core.Examples.Pages.GridMove
{
    public partial class ColumnMoveGroupFieldModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_ColumnMove")
            {
                var param = e.EventArgumentsAsJObject;
                // 模拟操作数据库中的数据
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, param.Value<JArray>("columnIds"));
            }
        }



        #region LoadData

        private void LoadData()
        {
            


            JArray savedColumns = GetSavedColumns();
            if (savedColumns != null && savedColumns.Count > 0)
            {
                // 将数据库保存的列顺序应用到表格列上
                ApplyOrders(Grid1, savedColumns);
            }

            Grid1.DataSource = GetDataTable();
            Grid1.DataBind();
        }

        private void ApplyOrders(FineUI.Core.Grid grid1, JArray savedColumns)
        {
            int order = 0;
            foreach (JObject column in savedColumns)
            {
                GridColumn foundColumn = grid1.FindColumn(column.Value<string>("columnId"));
                if (foundColumn != null)
                {
                    foundColumn.ColumnOrder = order;
                }

                JArray columns = column.Value<JArray>("columns");
                if (columns != null)
                {
                    ApplyOrders(grid1, columns);
                }

                order++;
            }
        }

        protected DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Guid", typeof(Guid)));
            table.Columns.Add(new DataColumn("Year", typeof(int)));
            table.Columns.Add(new DataColumn("HZData1", typeof(int)));
            table.Columns.Add(new DataColumn("HZData2", typeof(int)));
            table.Columns.Add(new DataColumn("HLData1", typeof(int)));
            table.Columns.Add(new DataColumn("HLData2", typeof(int)));
            table.Columns.Add(new DataColumn("AHData1", typeof(int)));
            table.Columns.Add(new DataColumn("AHData2", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));


            DataRow row;

            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int year = 2000 + i;

                row = table.NewRow();
                row[0] = 100 + i;
                row[1] = Guid.NewGuid();
                row[2] = year;
                row[3] = rd.Next(1000, 9999);
                row[4] = rd.Next(1000, 9999);
                row[5] = rd.Next(1000, 9999);
                row[6] = rd.Next(1000, 9999);
                row[7] = rd.Next(1000, 9999);
                row[8] = rd.Next(1000, 9999);
                row[9] = DateTime.Parse(String.Format("{0}-09-01", year));

                table.Rows.Add(row);
            }

            return table;
        } 
        #endregion

        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridMove.ColumnMoveGroupField";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSavedColumns()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                //[{
                //    "columnId": "Year"
                //}, {
                //    "columnId": "anhui",
                //    "columns": [{
                //        "columnId": "hefei",
                //        "columns": [{
                //            "columnId": "AHData1"
                //        }, {
                //            "columnId": "AHData2"
                //        }]
                //    }]
                //}, {
                //    "columnId": "henan",
                //    "columns": [{
                //        "columnId": "zhumadian",
                //        "columns": [{
                //            "columnId": "HZData1"
                //        }, {
                //            "columnId": "HZData2"
                //        }]
                //    }, {
                //        "columnId": "luohe",
                //        "columns": [{
                //            "columnId": "HLData1"
                //        }, {
                //            "columnId": "HLData2"
                //        }]
                //    }]
                //}, {
                //    "columnId": "LogTime"
                //}]
                HttpContext.Session.SetObject<JArray>(KEY_FOR_DATASOURCE_SESSION, JArray.Parse("[{\"columnId\":\"Year\"},{\"columnId\":\"anhui\",\"columns\":[{\"columnId\":\"hefei\",\"columns\":[{\"columnId\":\"AHData1\"},{\"columnId\":\"AHData2\"}]}]},{\"columnId\":\"henan\",\"columns\":[{\"columnId\":\"zhumadian\",\"columns\":[{\"columnId\":\"HZData1\"},{\"columnId\":\"HZData2\"}]},{\"columnId\":\"luohe\",\"columns\":[{\"columnId\":\"HLData1\"},{\"columnId\":\"HLData2\"}]}]},{\"columnId\":\"LogTime\"}]"));
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION);
        }

        
        #endregion

    }
}