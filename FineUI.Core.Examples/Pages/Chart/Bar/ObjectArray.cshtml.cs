using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class ObjectArrayModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarObjectArray.DataSource = new[]
                {
                    new { Category = "Matcha Latte", Year2015 = 43.3, Year2016 = 85.8, Year2017 = 93.7 },
                    new { Category = "Milk Tea", Year2015 = 83.1, Year2016 = 73.4, Year2017 = 55.1 },
                    new { Category = "Cheese Cocoa", Year2015 = 86.4, Year2016 = 65.2, Year2017 = 82.5 },
                    new { Category = "Walnut Brownie", Year2015 = 72.4, Year2016 = 53.9, Year2017 = 39.1 }
                };
                ChartBarObjectArray.DataBind();
            }
        }
    }
}
