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
    public partial class ColumnMoveOrderWidthHiddenModel : BaseModel
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

        private void LoadData()
        {
            JArray savedColumns = GetSavedColumns();
            if (savedColumns != null && savedColumns.Count > 0)
            {
                // 将数据库保存的列顺序应用到表格列上
                int order = 0;
                foreach (JObject column in savedColumns)
                {
                    GridColumn foundColumn = Grid1.FindColumn(column.Value<string>("columnId"));
                    if (foundColumn != null)
                    {
                        foundColumn.ColumnOrder = order;

                        // hidden 属性可能不存在
                        bool? hidden = column.Value<bool?>("hidden");
                        if (hidden != null && hidden.Value)
                        {
                            foundColumn.Hidden = true;
                        }

                        // flex 属性可能不存在
                        int? flex = column.Value<int?>("flex");
                        if (flex != null)
                        {
                            foundColumn.BoxFlex = flex.Value;
                        }
                        else
                        {
                            // width 属性可能不存在
                            int? width = column.Value<int?>("width");
                            if (width != null)
                            {
                                foundColumn.Width = width.Value;
                            }
                        }

                    }
                    order++;
                }
            }

            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }


        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridMove.ColumnMoveOrderWidthHidden";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSavedColumns()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                //[{
                //    "columnId": "RowNumber",
                //    "width": 30
                //}, {
                //    "columnId": "Name",
                //    "width": 100
                //}, {
                //    "columnId": "Gender",
                //    "width": 80
                //}, {
                //    "columnId": "EntranceYear",
                //    "width": 80
                //}, {
                //    "columnId": "AtSchool",
                //    "width": 80
                //}, {
                //    "columnId": "Major",
                //    "flex": 1
                //}, {
                //    "columnId": "LogTime",
                //    "width": 100
                //}]
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, JArray.Parse("[{\"columnId\":\"RowNumber\",\"width\":30},{\"columnId\":\"Name\",\"width\":100},{\"columnId\":\"Gender\",\"width\":80},{\"columnId\":\"EntranceYear\",\"width\":80},{\"columnId\":\"AtSchool\",\"width\":80},{\"columnId\":\"Major\",\"flex\":1},{\"columnId\":\"LogTime\",\"width\":100}]"));
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION);
        }


        #endregion

    }
}