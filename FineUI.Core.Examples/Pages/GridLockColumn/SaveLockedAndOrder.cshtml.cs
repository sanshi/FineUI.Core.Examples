using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.GridLockColumn
{
    public partial class SaveLockedAndOrderModel : BaseModel
    {
        private static readonly JArray GRID_COLUMNCONFIG_DEFAULT = new JArray
        {
            new JObject {
                { "ColumnID", "RowNumber" },
                { "Locked", true }
            },
            new JObject {
                { "ColumnID", "Name" },
                { "Locked", true }
            },
            new JObject {
                { "ColumnID", "Gender" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "EntranceYear" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "AtSchool" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "Major" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "ShenGao" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "TiZhong" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "XueYaDi" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "XueYaGao" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "ShiLiZuo" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "ShiLiYou" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "Group" },
                { "Locked", false }
            },
            new JObject {
                { "ColumnID", "LogTime" },
                { "Locked", false }
            }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_ColumnLockUnlock")
            {
                var param = e.EventArgumentsAsJObject;

                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, param.Value<JArray>("configedColumns"));
            }
        }



        private void LoadData()
        {
            // 模拟操作数据库中的数据
            JArray savedColumns = GetSavedColumns();

            // 将数据库保存的列顺序应用到表格列上
            int order = 0;
            foreach (JObject column in savedColumns)
            {
                GridColumn foundColumn = Grid1.FindColumn(column.Value<string>("ColumnID"));
                if (foundColumn != null)
                {
                    foundColumn.ColumnOrder = order;

                    // 约定：Locked 属性一定存在
                    bool locked = column.Value<bool>("Locked");
                    foundColumn.Locked = locked;
                }
                order++;
            }


            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }


        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridLockColumn.SaveLockedAndOrder";

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