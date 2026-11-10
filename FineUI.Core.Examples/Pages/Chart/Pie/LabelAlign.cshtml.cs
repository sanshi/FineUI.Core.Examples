using System;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class LabelAlignModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var rows = new[]
                {
                    new { Category = "Apples", Value = 70 },
                    new { Category = "Strawberries", Value = 68 },
                    new { Category = "Bananas", Value = 48 },
                    new { Category = "Oranges", Value = 40 },
                    new { Category = "Pears", Value = 32 },
                    new { Category = "Pineapples", Value = 27 },
                    new { Category = "Grapes", Value = 18 }
                };
                ChartPieLabelAlignNone.DataSource = rows;
                ChartPieLabelAlignNone.DataBind();
                ChartPieLabelAlignLine.DataSource = rows;
                ChartPieLabelAlignLine.DataBind();
                ChartPieLabelAlignEdge.DataSource = rows;
                ChartPieLabelAlignEdge.DataBind();
            }
        }
    }
}
