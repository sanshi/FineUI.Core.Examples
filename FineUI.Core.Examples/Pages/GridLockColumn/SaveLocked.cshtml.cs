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
    public partial class SaveLockedModel : BaseModel
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
            if (e.EventName == "Grid1_ColumnLockUnlock")
            {
                var param = e.EventArgumentsAsJObject;

                var type = param.Value<string>("type");
                var columnId = param.Value<string>("columnId");
                // 模拟操作数据库中的数据
                List<string> lockedColumns = GetLockedColumns();
                if (type == "lock")
                {
                    if (!lockedColumns.Contains(columnId))
                    {
                        lockedColumns.Add(columnId);
                    }
                }
                else if (type == "unlock")
                {
                    if (lockedColumns.Contains(columnId))
                    {
                        lockedColumns.Remove(columnId);
                    }
                }

                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, lockedColumns);
            }
        }



        private void LoadData()
        {
            List<string> lockedColumns = GetLockedColumns();
            if (lockedColumns.Count > 0)
            {
                foreach (GridColumn column in Grid1.Columns)
                {
                    RenderBaseField field = column as RenderBaseField;
                    if (field == null)
                    {
                        continue;
                    }

                    if (lockedColumns.Contains(field.ColumnID) || lockedColumns.Contains(field.DataField))
                    {
                        field.Locked = true;
                    }

                }
            }


            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }


        #region Data

        private static readonly string KEY_FOR_DATASOURCE_SESSION = "GridLockColumn.SaveLocked";

        // 模拟在服务器端保存数据
        // 特别注意：在真实的开发环境中，不要在Session放置大量数据，否则会严重影响服务器性能
        private List<string> GetLockedColumns()
        {
            if (HttpContext.Session.GetObject<List<string>>(KEY_FOR_DATASOURCE_SESSION) == null)
            {
                HttpContext.Session.SetObject(KEY_FOR_DATASOURCE_SESSION, new List<string>() { "RowNumber", "Name" });
            }
            return HttpContext.Session.GetObject<List<string>>(KEY_FOR_DATASOURCE_SESSION);
        }


        #endregion

    }
}