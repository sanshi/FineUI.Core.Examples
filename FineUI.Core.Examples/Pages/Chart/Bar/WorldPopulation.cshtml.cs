using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class WorldPopulationModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarWorldPopulation.DataSource = new[]
                {
                    new { Category = "Brazil", Population2011 = 18203, Population2012 = 19325 },
                    new { Category = "Indonesia", Population2011 = 23489, Population2012 = 23438 },
                    new { Category = "USA", Population2011 = 29034, Population2012 = 31000 },
                    new { Category = "India", Population2011 = 104970, Population2012 = 121594 },
                    new { Category = "China", Population2011 = 131744, Population2012 = 134141 },
                    new { Category = "World", Population2011 = 630230, Population2012 = 681807 }
                };
                ChartBarWorldPopulation.DataBind();
            }
        }
    }
}
