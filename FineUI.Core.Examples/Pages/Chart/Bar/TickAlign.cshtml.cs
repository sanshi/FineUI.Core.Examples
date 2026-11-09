using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class TickAlignModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarTickAlign.DataSource = new[]
                {
                    new { Category = "Mon", Value = 10 },
                    new { Category = "Tue", Value = 52 },
                    new { Category = "Wed", Value = 200 },
                    new { Category = "Thu", Value = 334 },
                    new { Category = "Fri", Value = 390 },
                    new { Category = "Sat", Value = 330 },
                    new { Category = "Sun", Value = 220 }
                };
                ChartBarTickAlign.DataBind();
            }
        }
    }
}
