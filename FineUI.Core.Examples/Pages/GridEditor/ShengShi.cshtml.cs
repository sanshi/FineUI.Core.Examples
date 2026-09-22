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
    public partial class ShengShiModel : BaseModel
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

            RegisterStartupScript(String.Format("window._SHI={0};", DataSourceUtil.SHI_JSON.ToString()));
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

            UpdateDataRow("Name", rowDict, rowData);
            UpdateDataRow("Gender", rowDict, rowData);
            UpdateDataRow("Sheng", rowDict, rowData);
            UpdateDataRow("Shi", rowDict, rowData);
        }

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridEditor.ShengShi";

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
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("Sheng", typeof(string)));
            table.Columns.Add(new DataColumn("Shi", typeof(string)));

            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = "张萍萍";
            row[2] = 0;
            row[3] = "河南";
            row[4] = "驻马店市";
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 1;
            row[3] = "安徽";
            row[4] = "合肥市";
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 0;
            row[3] = "北京";
            row[4] = "北京市";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 104;
            row[1] = "刘国";
            row[2] = 0;
            row[3] = "";
            row[4] = "";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 105;
            row[1] = "康颖颖";
            row[2] = 0;
            row[3] = "";
            row[4] = "";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 106;
            row[1] = "彭博";
            row[2] = 1;
            row[3] = "";
            row[4] = "";
            table.Rows.Add(row);


            return table;
        }

        #endregion

    }
}