using System;

namespace FineUI.Core.Examples.Pages.GridBigData
{
    public partial class BigData10000RowTipModel : BaseModel
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
            Grid1.DataSource = BigDataUtil.GetBigData(10000);
            Grid1.DataBind();
        }




    }
}
