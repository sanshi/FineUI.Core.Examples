using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class RowDataBoundServerModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Grid1_RowDataBound(object sender, GridRowDataBoundEventArgs e)
        {
            // e.DataItem - 行数据源
            // 1. 如果表格数据源为 DataTable，则行数据源为 DataRowView
            // 2. 如果表格数据源为 List<Role>，则行数据源为模型类 Role
            //var row = e.DataItem as DataRowView;
            //var entranceYear = Convert.ToDateTime(row["LogTime"]);


            // 注意：GetFieldValue的参数是数据字段的名称（DataField），而不是列名（ColumnID）！
            var logTime = Convert.ToDateTime(e.GetFieldValue("LogTime"));
            e.SetFieldValue("LogTime", logTime.ToString("yyyy年MM月dd日"));


            var gender = Convert.ToInt32(e.GetFieldValue("Gender"));
            e.SetFieldValue("Gender", gender == 1 ? "男" : "女");
        }



        protected void btnClearData_Click(object sender, EventArgs e)
        {
            Grid1.DataSource = null;
            Grid1.DataBind();
        }

        protected void btnRebindData_Click(object sender, EventArgs e)
        {
            Grid1.DataSource = DataSourceUtil.GetDataTable2();
            Grid1.DataBind();
        }


    }
}