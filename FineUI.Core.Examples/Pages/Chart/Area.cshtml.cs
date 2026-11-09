using System;

namespace FineUI.Core.Examples.Pages.Chart
{
    public partial class AreaModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartArea.DataSource = new[]
                {
                    new { Category = "Mon", Value = 820 },
                    new { Category = "Tue", Value = 932 },
                    new { Category = "Wed", Value = 901 },
                    new { Category = "Thu", Value = 934 },
                    new { Category = "Fri", Value = 1290 },
                    new { Category = "Sat", Value = 1330 },
                    new { Category = "Sun", Value = 1320 }
                };
                ChartArea.DataBind();
            }
        }
    }
}
