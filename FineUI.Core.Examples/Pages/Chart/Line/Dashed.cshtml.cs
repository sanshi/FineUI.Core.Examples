using System;

namespace FineUI.Core.Examples.Pages.Chart.Line
{
    public partial class DashedModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartLineDashed.DataSource = new[]
                {
                    new { Category = "A", Value = 10 },
                    new { Category = "B", Value = 22 },
                    new { Category = "C", Value = 28 },
                    new { Category = "D", Value = 23 },
                    new { Category = "E", Value = 19 }
                };
                ChartLineDashed.DataBind();
            }
        }
    }
}
