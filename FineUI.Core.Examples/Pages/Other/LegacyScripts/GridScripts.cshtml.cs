using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class GridScriptsModel : LegacyScriptPageBase
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
            Grid1.DataSource = new[] { new { Id = "105", Name = "客户甲" } };
            Grid1.DataBind();
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            // 每次追加使用独立行标识，连续点击也不会覆盖前一次新增的客户。
            var customers = new[] { new { Id = Guid.NewGuid().ToString("N"), Name = "新增客户" } };
            RegisterStartupScript(Grid1.GetAppendDataReference(customers));
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            RegisterStartupScript(Grid1.GetUpdateCellValueReference(0, "Name", "客户甲（已修改）"));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
