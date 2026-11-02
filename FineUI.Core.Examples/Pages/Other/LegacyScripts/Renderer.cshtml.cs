using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class RendererModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("客户甲");
            }
        }

        private void LoadData(string name)
        {
            Grid1.DataSource = new[] { new { Id = "105", Name = name } };
            Grid1.DataBind();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            LoadData("客户乙");
        }
    }
}
