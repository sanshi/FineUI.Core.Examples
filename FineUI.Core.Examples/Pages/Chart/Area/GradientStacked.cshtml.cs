using System;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class GradientStackedModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartAreaGradientStacked.DataSource = new[]
                {
                    new { Category = "Mon", Line1 = 140, Line2 = 120, Line3 = 320, Line4 = 220, Line5 = 220 },
                    new { Category = "Tue", Line1 = 232, Line2 = 282, Line3 = 132, Line4 = 402, Line5 = 302 },
                    new { Category = "Wed", Line1 = 101, Line2 = 111, Line3 = 201, Line4 = 231, Line5 = 181 },
                    new { Category = "Thu", Line1 = 264, Line2 = 234, Line3 = 334, Line4 = 134, Line5 = 234 },
                    new { Category = "Fri", Line1 = 90, Line2 = 220, Line3 = 190, Line4 = 190, Line5 = 210 },
                    new { Category = "Sat", Line1 = 340, Line2 = 340, Line3 = 130, Line4 = 230, Line5 = 290 },
                    new { Category = "Sun", Line1 = 250, Line2 = 310, Line3 = 220, Line4 = 120, Line5 = 150 }
                };
                ChartAreaGradientStacked.DataBind();
            }
        }
    }
}
