using Microsoft.AspNetCore.Mvc;
using System;


namespace FineUI.Core.Examples.Pages.GridBigData
{
    public partial class BigData10000PagingDatabaseModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        #region BindGrid

        private void LoadData()
        {
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = 10000;

            // 2.获取当前分页数据
            Grid1.DataSource = BigDataUtil.GetBigData(10000, Grid1.PageIndex, Grid1.PageSize);
            Grid1.DataBind();
        }

        #endregion

        protected void Grid1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }




    }
}
