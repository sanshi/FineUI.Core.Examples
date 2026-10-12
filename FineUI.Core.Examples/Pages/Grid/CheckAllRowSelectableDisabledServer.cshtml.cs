using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckAllRowSelectableDisabledServerModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }



        protected void Grid1_RowDataBound(object sender, GridRowDataBoundEventArgs e)
        {
            // e.DataItem - 行数据源
            // 1. 如果表格数据源为 DataTable，则行数据源为 DataRowView
            // 2. 如果表格数据源为 List<Role>，则行数据源为模型类 Role
            var row = e.DataItem as DataRowView;
            var entranceYear = Convert.ToInt32(row["EntranceYear"]);

            if (entranceYear >= 2008)
            {
                e.RowSelectable = false;
            }
            else
            {
                e.RowSelectable = true;
            }
        }


    }
}