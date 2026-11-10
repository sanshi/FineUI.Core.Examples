using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class WaterfallModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarWaterfall.DataSource = new[]
                {
                    new { Category = "Total", Offset = 0, Cost = 2900 },
                    new { Category = "Rent", Offset = 1700, Cost = 1200 },
                    new { Category = "Utilities", Offset = 1400, Cost = 300 },
                    new { Category = "Transportation", Offset = 1200, Cost = 200 },
                    new { Category = "Meals", Offset = 300, Cost = 900 },
                    new { Category = "Other", Offset = 0, Cost = 300 }
                };
                ChartBarWaterfall.DataBind();
            }
        }
    }
}
