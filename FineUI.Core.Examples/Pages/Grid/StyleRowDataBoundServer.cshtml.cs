using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class StyleRowDataBoundServerModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoBindGrid();
            }
        }

        private void AutoBindGrid()
        {
            var sourceKey = Grid1.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "table2")
            {
                BindGrid();
                sourceKey = "table1";
            }
            else
            {
                BindGrid2();
                sourceKey = "table2";
            }

            Grid1.Attributes["data-source-key"] = sourceKey;
        }

        private void BindGrid()
        {
            DataTable table = DataSourceUtil.GetDataTable();

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        private void BindGrid2()
        {
            DataTable table = DataSourceUtil.GetDataTable2();

            Grid1.DataSource = table;
            Grid1.DataBind();
        }


        #region Events

        protected void Grid1_RowDataBound(object sender, GridRowDataBoundEventArgs e)
        {
            // e.DataItem - 行数据源
            // 1. 如果表格数据源为 DataTable，则行数据源为 DataRowView
            // 2. 如果表格数据源为 List<Role>，则行数据源为模型类 Role
            var row = e.DataItem as DataRowView;
            var entranceYear = Convert.ToInt32(row["EntranceYear"]);

            if (entranceYear >= 2000 && entranceYear <= 2004)
            {
                e.RowCssClass = "color1";
            }
            else if (entranceYear == 2008)
            {
                e.RowCssClass = "color3";
            }

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            AutoBindGrid();
        }

        #endregion
    }
}