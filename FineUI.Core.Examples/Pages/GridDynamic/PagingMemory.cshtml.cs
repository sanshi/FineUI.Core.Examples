using System;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class PagingMemoryModel : PageBase
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
            Grid1.AllowPaging = true;
            Grid1.PageIndex = 0;
            Grid1.PageSize = 5;
            Grid1.ShowPageSizeSelector = true;
            Grid1.PageSizeOptions = new[] { 3, 5, 10 };
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.AllowPaging = false;
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
            Grid1.PagingType = PagingType.NumberButton;
        }
    }
}
