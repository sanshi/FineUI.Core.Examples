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
    public partial class RowMoveColumnConfigModel : BaseModel
    {
        private static readonly JArray GRID_COLUMNCONFIG_DEFAULT = new JArray
        {
            new JObject {
                { "ColumnID", "RowNumber" },
                { "Hidden", true },
                { "HeaderText", "" }
            },
            new JObject {
                { "ColumnID", "Name" },
                { "Hidden", false },
                { "HeaderText", "姓名" }
            },
            new JObject {
                { "ColumnID", "Gender" },
                { "Hidden", false },
                { "HeaderText", "性别" }
            },
            new JObject {
                { "ColumnID", "EntranceYear" },
                { "Hidden", false },
                { "HeaderText", "入学年份" }
            },
            new JObject {
                { "ColumnID", "AtSchool" },
                { "Hidden", false },
                { "HeaderText", "是否在校" }
            },
            new JObject {
                { "ColumnID", "Major" },
                { "Hidden", false },
                { "HeaderText", "所学专业" }
            },
            new JObject {
                { "ColumnID", "LogTime" },
                { "Hidden", false },
                { "HeaderText", "注册日期" }
            }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
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
                    GridColumn foundColumn = Grid1.FindColumn(column.Value<string>("ColumnID"));
                    if (foundColumn != null)
                    {
                        foundColumn.ColumnOrder = order;

                        // 约定：Hidden 属性一定存在
                        bool hidden = column.Value<bool>("Hidden");
                        foundColumn.Hidden = hidden;

                        // 约定：HeaderText 属性一定存在
                        string headerText = column.Value<string>("HeaderText");
                        foundColumn.HeaderText = headerText;
                    }
                    order++;
                }
            }

            RegisterStartupScript(String.Format("window.GRID_COLUMNCONFIG_DEFAULT={0};", GRID_COLUMNCONFIG_DEFAULT.ToString(Newtonsoft.Json.Formatting.Indented)));
        }


        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "GridConfig_Change")
            {
                var param = e.EventArgumentsAsJObject;

                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, param.Value<JArray>("configedColumns"));
            }
        }



        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridMove.RowMoveColumnConfig";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSavedColumns()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, GRID_COLUMNCONFIG_DEFAULT);
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_DATASOURCE_SESSION);
        }


        #endregion
    }
}