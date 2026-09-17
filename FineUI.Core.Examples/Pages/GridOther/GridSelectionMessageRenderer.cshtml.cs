using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class GridSelectionMessageRendererModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        #region BindGrid

        private void LoadData()
        {
            var recordCount = DataSourceUtil.GetTotalCount();

            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = recordCount;

            // 2.获取当前分页数据
            Grid1.DataSource = DataSourceUtil.GetPagedDataTable(pageIndex: Grid1.PageIndex, pageSize: Grid1.PageSize);
            Grid1.DataBind();
        }

        #endregion

        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            LoadData();
        }

    }
}
