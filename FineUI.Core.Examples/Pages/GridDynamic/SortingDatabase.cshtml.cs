using System;
using System.Linq;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class SortingDatabaseModel : PageBase
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
            var data = DataSourceUtil.GetDataTable();
            // 用数据视图模拟数据库排序；实际项目在这里按当前排序条件查询数据库。
            if (Grid1.SortingMulti)
            {
                var fields = Grid1.SortFieldArray;
                data.DefaultView.Sort = String.Join(", ", Enumerable.Range(0, fields.Length / 2)
                    .Select(index => fields[index * 2] + " " + fields[index * 2 + 1]));
            }
            else if (!String.IsNullOrEmpty(Grid1.SortField))
            {
                data.DefaultView.Sort = Grid1.SortField + " " + Grid1.SortDirection;
            }
            Grid1.DataSource = data.DefaultView;
            Grid1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Grid1.AllowSorting = true;
            Grid1.SortingMulti = false;
            Grid1.SortField = "EntranceYear";
            Grid1.SortDirection = "DESC";
            LoadData();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.AllowSorting = false;
            Grid1.SortingMulti = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LabelResult.Text = $"当前列数：{Grid1.Columns.Count}；分页：{Grid1.AllowPaging}；页大小：{Grid1.PageSize}；排序：{Grid1.AllowSorting}；多选：{Grid1.EnableMultiSelect}";
        }

        protected void BusinessButton_Click(object sender, EventArgs e)
        {
            LabelResult.Text = "业务按钮仍可回发。";
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Grid1.AllowSorting = true;
            Grid1.SortingMulti = true;
            Grid1.SortFieldArray = new[] { "EntranceYear", "DESC", "Name", "ASC" };
            LoadData();
        }
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            LoadData();
            LabelResult.Text = "数据库排序事件已执行。";
        }
    }
}
