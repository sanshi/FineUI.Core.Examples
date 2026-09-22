using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridEditor
{
    public partial class NumberBoxEmptyModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        private void LoadData()
        {
            Grid1.DataSource = GetSourceData();
            Grid1.DataBind();
        }





        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            JArray modifiedData = Grid1.GetModifiedData();

            DataTable source = GetSourceData();

            foreach (JObject modifiedRow in modifiedData)
            {
                string status = modifiedRow.Value<string>("status");
                int rowId = Convert.ToInt32(modifiedRow.Value<string>("id"));

                if (status == "modified")
                {
                    UpdateDataRow(modifiedRow, rowId, source);
                }
            }

            Grid1.DataSource = source;
            Grid1.DataBind();

            labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", EncodeJson(modifiedData));

            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, source);

            ShowNotify("数据保存成功！（表格数据已重新绑定）");
        }

        #region UpdateDataRow

        private void UpdateDataRow(JObject modifiedRow, int rowId, DataTable source)
        {
            Dictionary<string, object> rowDict = modifiedRow.Value<JObject>("values").ToObject<Dictionary<string, object>>();
            DataRow rowData = FindRowByID(source, rowId);

            UpdateDataRow("Year", rowDict, rowData);
            UpdateDataRow("XueFei", rowDict, rowData);
            UpdateDataRow("ZhusuFei", rowDict, rowData);
            UpdateDataRow("HuoshiFei", rowDict, rowData);
            UpdateDataRow("ShubenFei", rowDict, rowData);
        }

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridEditor.NumberBoxEmpty";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }

        public static DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Year", typeof(int)));
            table.Columns.Add(new DataColumn("XueFei", typeof(int)));
            table.Columns.Add(new DataColumn("ZhusuFei", typeof(int)));
            table.Columns.Add(new DataColumn("HuoshiFei", typeof(int)));
            table.Columns.Add(new DataColumn("ShubenFei", typeof(int)));

            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = 2014;
            row[2] = 1000;
            row[3] = 3000;
            row[4] = 2000;
            row[5] = 1000;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 102;
            row[1] = 2015;
            row[2] = 1200;
            row[3] = 3200;
            row[4] = 2200;
            row[5] = 1200;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 103;
            row[1] = 2016;
            row[2] = 1500;
            row[3] = 3500;
            row[4] = 2500;
            row[5] = 1500;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 104;
            row[1] = 2017;
            row[2] = DBNull.Value;
            row[3] = DBNull.Value;
            row[4] = DBNull.Value;
            row[5] = DBNull.Value;
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = 2018;
            row[2] = DBNull.Value;
            row[3] = DBNull.Value;
            row[4] = DBNull.Value;
            row[5] = DBNull.Value;
            table.Rows.Add(row);


            return table;
        }

        #endregion

    }
}