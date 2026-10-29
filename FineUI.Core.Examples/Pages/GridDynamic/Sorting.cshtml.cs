using System;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class SortingModel : PageBase
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
            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Grid1.AllowSorting = true;
            Grid1.SortingMulti = false;
            Grid1.SortField = "EntranceYear";
            Grid1.SortDirection = "DESC";
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
        }
    }
}
