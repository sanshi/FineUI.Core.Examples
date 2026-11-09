using System;

namespace FineUI.Core.Examples.Pages.Chart
{
    public partial class LineModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartLine.DataSource = new[]
                {
                    new { Category = "Mon", Value = 150 },
                    new { Category = "Tue", Value = 230 },
                    new { Category = "Wed", Value = 224 },
                    new { Category = "Thu", Value = 218 },
                    new { Category = "Fri", Value = 135 },
                    new { Category = "Sat", Value = 147 },
                    new { Category = "Sun", Value = 260 }
                };
                ChartLine.DataBind();
            }
        }
    }
}
