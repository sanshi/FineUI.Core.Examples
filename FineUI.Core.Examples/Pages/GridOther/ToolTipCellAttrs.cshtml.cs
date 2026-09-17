using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class ToolTipCellAttrsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 修改最后一行的数据
            DataTable table = DataSourceUtil.GetDataTable();
            DataRow lastRow = table.Rows[table.Rows.Count - 1];
            lastRow["Major"] = "<b>" + lastRow["Major"] + "&</b>";

            Grid1.DataSource = table;
            Grid1.DataBind();
        }



    }
}