using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckAllShowSelectedRowModel : PageBase
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
