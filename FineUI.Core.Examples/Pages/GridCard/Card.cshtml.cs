using System;

namespace FineUI.Core.Examples.Pages.GridCard
{
    public partial class CardModel : BaseModel
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
