using System;

namespace FineUI.Core.Examples.Pages.GridMove
{
    public partial class RowSortModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.EnableRowMove = true;
                BindTasks();
            }
        }

        private void BindTasks()
        {
            Grid1.DataSource = new[]
            {
                new { Id = "requirements", Name = "需求确认" },
                new { Id = "design", Name = "设计评审" },
                new { Id = "implementation", Name = "开发实现" },
                new { Id = "delivery", Name = "交付验收" }
            };
            Grid1.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindTasks();
            labReset.Text = Grid1.EnableRowMove ? "已恢复初始顺序，仍可拖拽排序。" : "已恢复初始顺序。";
        }
    }
}
