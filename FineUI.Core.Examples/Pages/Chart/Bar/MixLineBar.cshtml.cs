using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class MixLineBarModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarMixLineBar.DataSource = new[]
                {
                    new { Category = "Mon", Evaporation = 2.0, Precipitation = 2.6, Temperature = 2.0 },
                    new { Category = "Tue", Evaporation = 4.9, Precipitation = 5.9, Temperature = 2.2 },
                    new { Category = "Wed", Evaporation = 7.0, Precipitation = 9.0, Temperature = 3.3 },
                    new { Category = "Thu", Evaporation = 23.2, Precipitation = 26.4, Temperature = 4.5 },
                    new { Category = "Fri", Evaporation = 25.6, Precipitation = 28.7, Temperature = 6.3 },
                    new { Category = "Sat", Evaporation = 76.7, Precipitation = 70.7, Temperature = 10.2 },
                    new { Category = "Sun", Evaporation = 135.6, Precipitation = 175.6, Temperature = 20.3 }
                };
                ChartBarMixLineBar.DataBind();
            }
        }
    }
}
