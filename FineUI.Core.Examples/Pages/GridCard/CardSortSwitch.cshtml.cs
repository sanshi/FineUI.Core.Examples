using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridCard
{
    public partial class CardSortSwitchModel : BaseModel
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
            Grid1.DataSource = GetSortedDataTable(Grid1.SortField, Grid1.SortDirection);
            Grid1.DataBind();
        }

        private DataTable GetSortedDataTable(string sortField, string sortDirection)
        {
            // 模拟数据排序
            DataTable table = DataSourceUtil.GetDataTable();
            DataView view1 = table.DefaultView;
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection);

            return view1.ToTable();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            LoadData();
        }


    }
}
