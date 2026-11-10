using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class SpacingModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarSpacing.DataSource = new[]
                {
                    new { Category = "A", First = 23, Second = 12 },
                    new { Category = "B", First = 24, Second = 14 },
                    new { Category = "C", First = 18, Second = 9 },
                    new { Category = "D", First = 25, Second = 9 },
                    new { Category = "E", First = 18, Second = 11 }
                };
                ChartBarSpacing.DataBind();
            }
        }
    }
}
