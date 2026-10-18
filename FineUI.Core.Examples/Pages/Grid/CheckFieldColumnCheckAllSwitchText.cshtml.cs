using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckFieldColumnCheckAllSwitchTextModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }


    }
}
