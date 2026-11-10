using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class NegativeModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarNegative.DataSource = new[]
                {
                    new { Category = "Mon", Profit = 200, Income = 320, Expenses = -120 },
                    new { Category = "Tue", Profit = 170, Income = 302, Expenses = -132 },
                    new { Category = "Wed", Profit = 240, Income = 341, Expenses = -101 },
                    new { Category = "Thu", Profit = 244, Income = 374, Expenses = -134 },
                    new { Category = "Fri", Profit = 200, Income = 390, Expenses = -190 },
                    new { Category = "Sat", Profit = 220, Income = 450, Expenses = -230 },
                    new { Category = "Sun", Profit = 210, Income = 420, Expenses = -210 }
                };
                ChartBarNegative.DataBind();
            }
        }
    }
}
