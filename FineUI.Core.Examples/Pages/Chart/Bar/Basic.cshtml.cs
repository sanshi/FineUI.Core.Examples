using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class BasicModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBar.DataSource = new[]
                {
                    new { Category = "Mon", Value = 120 },
                    new { Category = "Tue", Value = 200 },
                    new { Category = "Wed", Value = 150 },
                    new { Category = "Thu", Value = 80 },
                    new { Category = "Fri", Value = 70 },
                    new { Category = "Sat", Value = 110 },
                    new { Category = "Sun", Value = 130 }
                };
                ChartBar.DataBind();
            }
        }
    }
}
