using System;

namespace FineUI.Core.Examples.Pages.Chart.Area
{
    public partial class StackedModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartAreaStacked.DataSource = new[]
                {
                    new
                    {
                        Category = "Mon",
                        Email = 120, UnionAds = 220, VideoAds = 150, Direct = 320, SearchEngine = 820
                    },
                    new
                    {
                        Category = "Tue",
                        Email = 132, UnionAds = 182, VideoAds = 232, Direct = 332, SearchEngine = 932
                    },
                    new
                    {
                        Category = "Wed",
                        Email = 101, UnionAds = 191, VideoAds = 201, Direct = 301, SearchEngine = 901
                    },
                    new
                    {
                        Category = "Thu",
                        Email = 134, UnionAds = 234, VideoAds = 154, Direct = 334, SearchEngine = 934
                    },
                    new
                    {
                        Category = "Fri",
                        Email = 90, UnionAds = 290, VideoAds = 190, Direct = 390, SearchEngine = 1290
                    },
                    new
                    {
                        Category = "Sat",
                        Email = 230, UnionAds = 330, VideoAds = 330, Direct = 330, SearchEngine = 1330
                    },
                    new
                    {
                        Category = "Sun",
                        Email = 210, UnionAds = 310, VideoAds = 410, Direct = 320, SearchEngine = 1320
                    }
                };
                ChartAreaStacked.DataBind();
            }
        }
    }
}
