using System;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class LayoutSummaryModel : PageBase
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
            Grid1.AllowColumnLocking = true;
            Grid1.FindColumn("Name").Locked = true;
            Grid1.EnableSummary = true;
            Grid1.SummaryPosition = SummaryPosition.Top;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.FindColumn("Name").Locked = false;
            Grid1.AllowColumnLocking = false;
            Grid1.EnableSummary = false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LabelResult.Text = $"允许锁定列：{Grid1.AllowColumnLocking}；姓名列锁定：{Grid1.FindColumn("Name").Locked}；"
                + $"启用合计：{Grid1.EnableSummary}；合计位置：{Grid1.SummaryPosition}";
        }

        protected void BusinessButton_Click(object sender, EventArgs e)
        {
            LabelResult.Text = "业务按钮仍可回发。";
        }
    }
}
