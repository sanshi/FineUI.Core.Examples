using System;

namespace FineUI.Core.Examples.Pages.GridCard
{
    public partial class CardPagingModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable2();
                Grid1.DataBind();
            }
        }





    }
}
