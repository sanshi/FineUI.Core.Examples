using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Text;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class FormWindowCellEditShengShiModel : BaseModel
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
            BindGrid();

            RegisterShengShiScript();
        }


        // 省和市都用代码：省下拉列表的数据在这里下发，市下拉列表的数据按省代码分组，等前台选中省份后再换一批
        private void RegisterShengShiScript()
        {
            // 注册脚本（省数据）
            JArray shengData = new JArray();
            shengData.Add(new JArray("001", "北京"));
            shengData.Add(new JArray("002", "河南"));
            shengData.Add(new JArray("003", "河北"));
            shengData.Add(new JArray("004", "湖南"));
            shengData.Add(new JArray("005", "湖北"));
            shengData.Add(new JArray("006", "广西"));
            shengData.Add(new JArray("007", "安徽"));
            string shengScript = String.Format("window._SHENG={0};", shengData.ToString(Newtonsoft.Json.Formatting.None));

            // 注册脚本（市数据）：市代码 = 省代码 + 三位序号，名称沿用公共的省市数据
            JObject shiData = new JObject();
            foreach (JArray item in shengData)
            {
                string shengValue = item[0].Value<string>();
                string shengText = item[1].Value<string>();

                JArray newSheng = new JArray();
                JArray sheng = DataSourceUtil.SHI_JSON[shengText] as JArray;
                int num = 1;
                foreach (string shi in sheng)
                {
                    JArray newShi = new JArray();
                    newShi.Add(shengValue + PaddingLeft(num));
                    newShi.Add(shi);
                    num++;

                    newSheng.Add(newShi);
                }

                shiData[shengValue] = newSheng;
            }
            string shiScript = String.Format("window._SHI={0};", shiData.ToString(Newtonsoft.Json.Formatting.None));

            FineUI.Core.PageContext.RegisterPreStartupScript(shengScript + shiScript);
        }


        private string PaddingLeft(int value)
        {
            StringBuilder sb = new StringBuilder();

            string str = value.ToString();
            for (int i = 0, count = 3 - str.Length; i < count; i++)
            {
                sb.Append("0");
            }
            sb.Append(str);

            return sb.ToString();
        }


        // 保存数据：客户端的改动一次性提交上来，按行状态分别处理
        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (Grid1.ModifiedData.Count == 0)
            {
                labResult.Text = "";
                ShowNotify("表格数据没有变化！");
                return;
            }

            DataTable table = GetSourceData();

            // 修改与删除先处理；新增行要等删除处理完，行序才与客户端一致
            foreach (JObject modifiedRow in Grid1.ModifiedData)
            {
                string status = modifiedRow.Value<string>("status");

                if (status == "modified")
                {
                    int rowID = Convert.ToInt32(modifiedRow.Value<string>("id"));
                    DataRow row = FindRowByID(table, rowID);

                    UpdateDataRow(modifiedRow, row);
                }
                else if (status == "deleted")
                {
                    DeleteRowByID(table, Convert.ToInt32(modifiedRow.Value<string>("id")));
                }
            }


            // 新增行：客户端把它放在第几行，回发数据的 index 就是几，服务端照着插
            // （前提是表格不分页、也没在客户端排过序，否则 index 与数据源的行序对不上）
            foreach (JObject modifiedRow in Grid1.ModifiedData)
            {
                if (modifiedRow.Value<string>("status") == "newadded")
                {
                    table.Rows.InsertAt(CreateNewData(table, modifiedRow), modifiedRow.Value<int>("index"));
                }
            }

            labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", EncodeJson(Grid1.ModifiedData));

            // 把改过的数据写回Session
            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, table);

            BindGrid();

            ShowNotify("数据保存成功！（表格数据已重新绑定）");
        }


        #region BindGrid

        private void BindGrid()
        {
            Grid1.DataSource = GetSourceData();
            Grid1.DataBind();
        }


        private DataRow CreateNewData(DataTable table, JObject modifiedRow)
        {
            DataRow rowData = table.NewRow();

            // 设置行ID（模拟数据库的自增长列）
            rowData["Id"] = GetNextRowID(table);
            UpdateDataRow(modifiedRow, rowData);

            return rowData;
        }


        // 把一行改动过的单元格写进数据行（列名与表格列的 ColumnID 一致）
        private void UpdateDataRow(JObject modifiedRow, DataRow rowData)
        {
            Dictionary<string, object> rowDict = modifiedRow.Value<JObject>("values").ToObject<Dictionary<string, object>>();

            // 姓名
            UpdateDataRow("Name", rowDict, rowData);

            // 性别
            UpdateDataRow("Gender", rowDict, rowData);

            // 入学年份
            UpdateDataRow("EntranceYear", rowDict, rowData);

            // 入学日期
            UpdateDataRow("EntranceDate", rowDict, rowData);

            // 是否在校
            UpdateDataRow("AtSchool", rowDict, rowData);

            // 所学专业
            UpdateDataRow("Major", rowDict, rowData);

            // 省
            UpdateDataRow("Sheng", rowDict, rowData);

            // 市
            UpdateDataRow("Shi", rowDict, rowData);
        }

        #endregion


        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "Grid.FormWindowCellEditShengShi";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        // 取数据时顺便记一下：Session里存的是JSON文本，每次取出来都是一个新对象，
        // 所以改完数据必须调用 SetObject 写回去，下一次取出来才会是改过的内容
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }


        // 获取模拟表格（简单表格），比别的页面多出「省」和「市」两列，两列存的都是代码
        public static DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(string)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(string)));
            table.Columns.Add(new DataColumn("Sheng", typeof(string)));
            table.Columns.Add(new DataColumn("Shi", typeof(string)));

            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = "张萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 0;
            row[6] = "2000-09-01";
            row[7] = "001";
            row[8] = "001001";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 2000;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2001-09-01";
            row[7] = "003";
            row[8] = "003003";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 2000;
            row[3] = true;
            row[4] = "化学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            row[7] = "005";
            row[8] = "005010";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 104;
            row[1] = "刘国";
            row[2] = 2002;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2002-09-01";
            row[7] = "007";
            row[8] = "007001";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 112;
            row[1] = "张三石";
            row[2] = 2012;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 1;
            row[6] = "2000-09-01";
            row[7] = "002";
            row[8] = "002017";
            table.Rows.Add(row);

            return table;
        }

        #endregion
    }
}
