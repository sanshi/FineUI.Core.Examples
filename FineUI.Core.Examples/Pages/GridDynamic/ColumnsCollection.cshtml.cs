using System;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class ColumnsCollectionModel : PageBase
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
            var group = new GroupField { HeaderText = "学生信息" };
            group.Columns.Add(new RenderField { ColumnID = "NewName", DataField = "Name", HeaderText = "姓名" });
            group.Columns.Add(new RenderField { ColumnID = "NewYear", DataField = "EntranceYear", HeaderText = "入学年份", FieldType = FieldType.Int });
            Grid1.Columns = new GridColumnCollection { group };
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.Columns = new GridColumnCollection
            {
                new RenderField { ColumnID = "Name", DataField = "Name", HeaderText = "姓名" },
                new RenderField { ColumnID = "EntranceYear", DataField = "EntranceYear", HeaderText = "入学年份", FieldType = FieldType.Int }
            };
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
            Grid1.Columns.Clear();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            GridColumn column = Grid1.FindColumn("Name") ?? Grid1.FindColumn("NewName");
            if (column != null)
            {
                column.Width = 180;
                column.TextAlign = TextAlign.Center;
                column.HeaderTextAlign = TextAlign.Right;
            }
        }
    }
}
