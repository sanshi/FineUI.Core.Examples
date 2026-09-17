using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class DatabaseMoreModel : BaseModel
    {
        private static readonly int PAGESIZE = 5;

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
            Grid1.DataSource = DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: PAGESIZE);
            Grid1.DataBind();
        }

        #endregion

        protected void btnMore_Click(object sender, EventArgs e)
        {
            var dataIndex = Grid1.Attributes.Value<int>("data-index");

            dataIndex++;

            var pageCount = DataSourceUtil.GetPageCount(PAGESIZE);
            if (dataIndex <= pageCount - 1)
            {
                var dataSource = DataSourceUtil.GetPagedDataTable(pageIndex: dataIndex, pageSize: PAGESIZE);
                RegisterStartupScript(Grid1.GetAppendDataReference(dataSource)); // 追加数据

                Grid1.Attributes["data-index"] = dataIndex.ToString();
            }

            if (dataIndex == pageCount - 1)
            {
                btnMore.Enabled = false;
                btnMore.Text = "全部加载完毕";
            }

        }

    }
}