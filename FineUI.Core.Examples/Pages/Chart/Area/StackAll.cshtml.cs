using System;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class StackAllModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartAreaStackAll.DataSource = new[]
                {
                    new { Category = "1", Baseline = 3, Change = 1 },
                    new { Category = "2", Baseline = 1, Change = 1 },
                    new { Category = "3", Baseline = 2, Change = 1 },
                    new { Category = "4", Baseline = -1, Change = 1 },
                    new { Category = "5", Baseline = -3, Change = 1 },
                    new { Category = "6", Baseline = 1, Change = 1 },
                    new { Category = "7", Baseline = 3, Change = 1 },
                    new { Category = "8", Baseline = -3, Change = -1 },
                    new { Category = "9", Baseline = -1, Change = -1 },
                    new { Category = "10", Baseline = -2, Change = -1 },
                    new { Category = "11", Baseline = 1, Change = -1 },
                    new { Category = "12", Baseline = 3, Change = -1 },
                    new { Category = "13", Baseline = -1, Change = -1 },
                    new { Category = "14", Baseline = -3, Change = -1 }
                };
                ChartAreaStackAll.DataBind();
            }
        }
    }
}
