using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class ChangeConfirmFormTableModel : BaseModel
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


        protected void btnClosePostBack_Click(object sender, EventArgs e)
        {
            JArray modifiedData = Grid1.GetModifiedData();

            // 保存数据后，清空面板内表单字段的改变状态
            SimpleForm1.ClearDirty();

            // 保存表格数据，并重新绑定
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

            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, source);

            ShowNotify("表单和表格提交成功！");
        }


        #region UpdateDataRow

        private void UpdateDataRow(JObject modifiedRow, int rowId, DataTable source)
         {
            Dictionary<string, object> rowDict = modifiedRow.Value<JObject>("values").ToObject<Dictionary<string, object>>();
            DataRow rowData = FindRowByID(source, rowId);

            UpdateDataRow("Name", rowDict, rowData);
            UpdateDataRow("Gender", rowDict, rowData);
            UpdateDataRow("EntranceYear", rowDict, rowData);
            UpdateDataRow("EntranceDate", rowDict, rowData);
            UpdateDataRow("AtSchool", rowDict, rowData);
            UpdateDataRow("Major", rowDict, rowData);
        }

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "Form.ChangeConfirmFormTable";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, DataSourceUtil.GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }

        #endregion

    }
}