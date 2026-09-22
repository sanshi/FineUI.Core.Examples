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
    public partial class DataChangeModel : BaseModel
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

        protected void btnRejectChanges_Click(object sender, EventArgs e)
        {
            // 只撤销浏览器里未保存的编辑，不重绑，也不修改会话数据源。
            FineUI.Core.PageContext.RegisterStartupScript(Grid1.GetRejectChangesReference());
        }

        protected void btnReadChanges_Click(object sender, EventArgs e)
        {
            // 再次回发读取，验证浏览器撤销后不再提交旧修改。
            labResult.Text = "未保存的修改记录数：" + Grid1.GetModifiedData().Count;
        }

        protected void btnCommitChanges_Click(object sender, EventArgs e)
        {
            // 只接受当前客户端编辑结果；故意不写入会话，以便刷新核对数据源仍未变化。
            FineUI.Core.PageContext.RegisterStartupScript(Grid1.GetCommitChangesReference());
        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            JArray modifiedData = Grid1.GetModifiedData();

            DataTable source = GetSourceData();

            foreach (JObject modifiedRow in modifiedData)
            {
                string status = modifiedRow.Value<string>("status");
                string rowId = modifiedRow.Value<string>("id");

                if (status == "modified")
                {
                    UpdateDataRow(modifiedRow, Convert.ToInt32(rowId), source);
                }
                else if (status == "deleted")
                {
                    DeleteRowByID(source, Convert.ToInt32(rowId));
                }
            }
            // 新增行：客户端把它放在第几行，回发数据的 index 就是几，服务端照着插
            // （前提是表格不分页、也没在客户端排过序，否则 index 与数据源的行序对不上）
            foreach (JObject modifiedRow in modifiedData)
            {
                if (modifiedRow.Value<string>("status") == "newadded")
                {
                    source.Rows.InsertAt(CreateNewData(modifiedRow, source), modifiedRow.Value<int>("index"));
                }
            }

            Grid1.DataSource = source;
            Grid1.DataBind();

            labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", EncodeJson(modifiedData));

            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, source);

            ShowNotify("数据保存成功！（表格数据已重新绑定）");
        }

        #region UpdateDataRow

        private DataRow CreateNewData(JObject modifiedRow, DataTable source)
        {
            DataRow rowData = source.NewRow();

            // 设置行ID（模拟数据库的自增长列）
            rowData["Id"] = GetNextRowID(source);
            UpdateDataRow(modifiedRow, rowData);

            return rowData;
        }


        private void UpdateDataRow(JObject modifiedRow, int rowId, DataTable source)
        {
            UpdateDataRow(modifiedRow, FindRowByID(source, rowId));
        }

        private void UpdateDataRow(JObject modifiedRow, DataRow rowData)
        {
            Dictionary<string, object> rowDict = modifiedRow.Value<JObject>("values").ToObject<Dictionary<string, object>>();

            UpdateDataRow("Name", rowDict, rowData);
            UpdateDataRow("Gender", rowDict, rowData);
            UpdateDataRow("EntranceYear", rowDict, rowData);
            UpdateDataRow("EntranceDate", rowDict, rowData);
            UpdateDataRow("AtSchool", rowDict, rowData);
            UpdateDataRow("Major", rowDict, rowData);
        }


        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridEditor.DataChange";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, DataSourceUtil.GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }


        // 模拟数据库的自增长列
        #endregion

    }
}
