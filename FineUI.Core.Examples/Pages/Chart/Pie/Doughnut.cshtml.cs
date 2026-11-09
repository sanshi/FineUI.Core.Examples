using System;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class DoughnutModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartPieDoughnut.DataSource = new[]
                {
                    new { Category = "Search Engine", Value = 1048 },
                    new { Category = "Direct", Value = 735 },
                    new { Category = "Email", Value = 580 },
                    new { Category = "Union Ads", Value = 484 },
                    new { Category = "Video Ads", Value = 300 }
                };
                ChartPieDoughnut.DataBind();
            }
        }
    }
}
