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
    public partial class StyleCellRendererModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            AutoBindGrid();
        }

        
    }
}