using System;

namespace FineUI.Core.Examples.Pages.Chart
{
    public partial class CombinationModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartCombo.DataSource = new[]
                {
                    new { Category = "Jan", Evaporation = 2.0, Precipitation = 2.6, Temperature = 2.0 },
                    new { Category = "Feb", Evaporation = 4.9, Precipitation = 5.9, Temperature = 2.2 },
                    new { Category = "Mar", Evaporation = 7.0, Precipitation = 9.0, Temperature = 3.3 },
                    new { Category = "Apr", Evaporation = 23.2, Precipitation = 26.4, Temperature = 4.5 },
                    new { Category = "May", Evaporation = 25.6, Precipitation = 28.7, Temperature = 6.3 },
                    new { Category = "Jun", Evaporation = 76.7, Precipitation = 70.7, Temperature = 10.2 },
                    new { Category = "Jul", Evaporation = 135.6, Precipitation = 175.6, Temperature = 20.3 },
                    new { Category = "Aug", Evaporation = 162.2, Precipitation = 182.2, Temperature = 23.4 },
                    new { Category = "Sep", Evaporation = 32.6, Precipitation = 48.7, Temperature = 23.0 },
                    new { Category = "Oct", Evaporation = 20.0, Precipitation = 18.8, Temperature = 16.5 },
                    new { Category = "Nov", Evaporation = 6.4, Precipitation = 6.0, Temperature = 12.0 },
                    new { Category = "Dec", Evaporation = 3.3, Precipitation = 2.3, Temperature = 6.2 }
                };
                ChartCombo.DataBind();
            }
        }
    }
}
