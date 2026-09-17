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
    public partial class ColumnMoveLockColumnModel : BaseModel
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
            if (e.EventName == "Grid1_ColumnMoveOrLock")
            {
                var param = e.EventArgumentsAsJObject;

                // 模拟操作数据库中的数据
                HttpContext.Session.SetObject(KEY_FOR_COLUMN_IDS_SESSION, param.Value<JArray>("columnIds"));

                HttpContext.Session.SetObject(KEY_FOR_LOCKED_COLUMN_IDS_SESSION, param.Value<JArray>("lockedColumnIds"));
            }
        }

        private void LoadData()
        {
            List<string> savedColumnIds = GetSavedColumnIds().ToObject<List<string>>();
            List<string> savedLockedColumnIds = GetSavedLockedColumnIds().ToObject<List<string>>();
            if (savedColumnIds != null && savedColumnIds.Count > 0)
            {
                // 将数据库保存的值应用到表格列上
                foreach (GridColumn column in Grid1.Columns)
                {
                    var columnId = column.ColumnID;

                    // 列锁定
                    if (savedLockedColumnIds.Contains(columnId))
                    {
                        column.Locked = true;
                    }
                    else
                    {
                        column.Locked = false;
                    }

                    // 列顺序
                    if (savedColumnIds.Contains(columnId))
                    {
                        column.ColumnOrder = savedColumnIds.IndexOf(columnId);
                    }
                }
            }

            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }


        #region Data

        private static readonly string KEY_FOR_COLUMN_IDS_SESSION = "GridMove.ColumnMove.ColumnIds";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSavedColumnIds()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_COLUMN_IDS_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_COLUMN_IDS_SESSION, new JArray());
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_COLUMN_IDS_SESSION);
        }

        private static readonly string KEY_FOR_LOCKED_COLUMN_IDS_SESSION = "GridMove.ColumnMove.LockedColumnIds";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private JArray GetSavedLockedColumnIds()
        {
            if (HttpContext.Session.GetObject<JArray>(KEY_FOR_LOCKED_COLUMN_IDS_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_LOCKED_COLUMN_IDS_SESSION, new JArray());
            }
            return HttpContext.Session.GetObject<JArray>(KEY_FOR_LOCKED_COLUMN_IDS_SESSION);
        }


        #endregion

    }
}