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
    public partial class RowMoveModel : BaseModel
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
            if (e.EventName == "Grid1_RowMove")
            {
                var param = e.EventArgumentsAsJObject;

                Grid1_RowMove(param.Value<JArray>("rowIds"));
            }
        }


        private void LoadData()
        {
            Grid1.DataSource = GetSavedDataSource();
            Grid1.DataBind();
        }

        protected void Grid1_RowMove(JArray rowIds)
        {
            DataTable originalTable = DataSourceUtil.GetDataTable();
            DataTable newTable = originalTable.Clone();

            foreach (string rowId in rowIds)
            {
                DataRow rowInOriginalTable = FindRow(rowId, originalTable);
                if (rowInOriginalTable != null)
                {
                    newTable.ImportRow(rowInOriginalTable);
                }
            }

            HttpContext.Session.SetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION, newTable);

            ShowNotify("数据保存成功！");
        }

        private DataRow FindRow(string rowId, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["Id"].ToString() == rowId)
                {
                    return row;
                }
            }
            return null;
        }

        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridMove.RowMove";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private DataTable GetSavedDataSource()
        {
            if (HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, DataSourceUtil.GetDataTable());
            }
            return HttpContext.Session.GetObject<DataTable>(KEY_FOR_DATASOURCE_SESSION);
        }


        #endregion

    }
}