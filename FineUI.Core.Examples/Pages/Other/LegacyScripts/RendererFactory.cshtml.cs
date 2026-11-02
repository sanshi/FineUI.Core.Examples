using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class RendererFactoryModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = new[] { new { Id = "105", Amount = 128 } };
                Grid1.DataBind();
            }
        }
    }
}
