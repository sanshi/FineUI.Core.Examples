using System;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class FillColorModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartAreaFillColor.DataSource = new[]
                {
                    new { Category = "A", First = 10, Second = 25 },
                    new { Category = "B", First = 22, Second = 14 },
                    new { Category = "C", First = 28, Second = 23 },
                    new { Category = "D", First = 23, Second = 35 },
                    new { Category = "E", First = 19, Second = 10 }
                };
                ChartAreaFillColor.DataBind();
            }
        }
    }
}
