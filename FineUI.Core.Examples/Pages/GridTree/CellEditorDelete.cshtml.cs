using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridTree
{
    public partial class CellEditorDeleteModel : BaseModel
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
            DataTable source = GetSourceData();

            foreach (JObject modifiedRow in Grid1.ModifiedData)
            {
                string status = modifiedRow.Value<string>("status");
                string rowId = modifiedRow.Value<string>("id");

                if (status == "modified")
                {
                    UpdateDataRow(modifiedRow, Convert.ToInt32(rowId), source);
                }
                else if (status == "deleted")
                {
                    DeleteRowByIDWithChildren(source, Convert.ToInt32(rowId));
                }
            }

            Grid1.DataSource = source;
            Grid1.DataBind();

            labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", EncodeJson(Grid1.ModifiedData));

            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, source);

            ShowNotify("数据保存成功！（表格数据已重新绑定）");
        }

        #region UpdateDataRow

        private void UpdateDataRow(JObject modifiedRow, int rowId, DataTable source)
        {
            Dictionary<string, object> rowDict = modifiedRow.Value<JObject>("values").ToObject<Dictionary<string, object>>();
            DataRow rowData = FindRowByID(source, rowId);

            UpdateDataRow("Name", rowDict, rowData);
            UpdateDataRow("Type", rowDict, rowData);
            UpdateDataRow("Size", rowDict, rowData);
            UpdateDataRow("ModifyDate", rowDict, rowData);
        }

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridTree.CellEditorDelete";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private DataTable GetSourceData()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, DataSourceUtil.GetTreeDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }


        // 根据行ParentID来获取行数据
        private DataTable FindRowByParentID(DataTable table, int rowParentID)
        {
            DataTable result = table.Clone();
            foreach (DataRow row in table.Rows)
            {
                if (Convert.ToInt32(row["ParentId"]) == rowParentID)
                {
                    result.ImportRow(row);
                }
            }
            return result;
        }

        // 根据行ID来删除行数据（同时递归删除所有子节点）
        private void DeleteRowByIDWithChildren(DataTable table, int rowID)
        {
            DataRow found = FindRowByID(table, rowID);
            if (found != null)
            {
                // 删除本行
                table.Rows.Remove(found);

                // 查找并删除所有子项
                DataTable children = FindRowByParentID(table, rowID);
                foreach (DataRow row in children.Rows)
                {
                    DeleteRowByIDWithChildren(table, Convert.ToInt32(row["Id"]));
                }
            }
        }



        #endregion

    }
}