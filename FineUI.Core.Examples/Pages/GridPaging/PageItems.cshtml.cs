using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class PageItemsModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable2();
                Grid1.DataBind();
            }
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

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            Grid1.SelectAllRows();
        }

        protected void btnClearSelect_Click(object sender, EventArgs e)
        {
            Grid1.DeselectAllRows();
        }

    }
}