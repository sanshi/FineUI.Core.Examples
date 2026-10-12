using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridMerge
{
    public partial class ColumnsDependsFirstModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次取数并统一日期精度，回发只重建列声明，保留已有合并结果。
                DataTable source = DataSourceUtil.GetDataTable();

                foreach (DataRow row in source.Rows)
                {
                    DateTime logTime = (DateTime)row["LogTime"];
                    row["LogTime"] = new DateTime(logTime.Year, logTime.Month, logTime.Day);
                }

                Grid1.DataSource = source;
                Grid1.DataBind();
            }
        }


        

    }
}