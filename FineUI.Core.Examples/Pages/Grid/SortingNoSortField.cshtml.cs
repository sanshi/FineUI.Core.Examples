using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class SortingNoSortFieldModel : BaseModel
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
            // 判断是否启用排序
            if (!String.IsNullOrEmpty(Grid1.SortField))
            {
                Grid1.DataSource = GetSortedDataTable(Grid1.SortField, Grid1.SortDirection);
            }
            else
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
            }
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



        protected void Grid1_Sort(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}