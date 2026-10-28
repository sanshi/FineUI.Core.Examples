using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class HeaderChangePropertiesModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // 按声明中的 ColumnID 找到目标列，只改显示属性，无需重新查询或绑定表格数据。
            var column = Grid1.FindColumn("EntranceYear");
            column.HeaderText = "入学年份（已修改）";
            column.Width = 200;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var column = Grid1.FindColumn("EntranceYear");
            column.HeaderText = "入学年份";
            column.Width = 120;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var column = Grid1.FindColumn("EntranceYear");
            LabelResult.Text = $"当前标题：{column.HeaderText}；当前宽度：{column.Width} 像素";
        }
    }
}
