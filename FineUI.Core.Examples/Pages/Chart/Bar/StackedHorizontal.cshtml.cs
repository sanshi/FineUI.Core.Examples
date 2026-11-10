using System;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class StackedHorizontalModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarStackedHorizontal.DataSource = new[]
                {
                    new
                    {
                        Category = "Mon",
                        Direct = 320, MailAd = 120, AffiliateAd = 220,
                        VideoAd = 150, SearchEngine = 820
                    },
                    new
                    {
                        Category = "Tue",
                        Direct = 302, MailAd = 132, AffiliateAd = 182,
                        VideoAd = 212, SearchEngine = 832
                    },
                    new
                    {
                        Category = "Wed",
                        Direct = 301, MailAd = 101, AffiliateAd = 191,
                        VideoAd = 201, SearchEngine = 901
                    },
                    new
                    {
                        Category = "Thu",
                        Direct = 334, MailAd = 134, AffiliateAd = 234,
                        VideoAd = 154, SearchEngine = 934
                    },
                    new
                    {
                        Category = "Fri",
                        Direct = 390, MailAd = 90, AffiliateAd = 290,
                        VideoAd = 190, SearchEngine = 1290
                    },
                    new
                    {
                        Category = "Sat",
                        Direct = 330, MailAd = 230, AffiliateAd = 330,
                        VideoAd = 330, SearchEngine = 1330
                    },
                    new
                    {
                        Category = "Sun",
                        Direct = 320, MailAd = 210, AffiliateAd = 310,
                        VideoAd = 410, SearchEngine = 1320
                    }
                };
                ChartBarStackedHorizontal.DataBind();
            }
        }
    }
}
