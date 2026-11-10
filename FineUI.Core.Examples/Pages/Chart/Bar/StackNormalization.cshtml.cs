using System;
using System.Linq;
using System.Globalization;

namespace FineUI.Core.Examples.Pages.Chart.Bar
{
    public partial class StackNormalizationModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartBarStackNormalization.DataSource = GetRows();
                ChartBarStackNormalization.DataBind();
            }
        }

        private static object GetRows()
        {
            var categories = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            var visitsByDay = new[]
            {
                new[] { 100, 320, 220, 150, 820 },
                new[] { 302, 132, 182, 212, 832 },
                new[] { 301, 101, 191, 201, 901 },
                new[] { 334, 134, 234, 154, 934 },
                new[] { 390, 90, 290, 190, 1290 },
                new[] { 330, 230, 330, 330, 1330 },
                new[] { 320, 210, 310, 410, 1320 }
            };
            return categories.Select((category, index) =>
            {
                var counts = visitsByDay[index];
                var total = counts.Sum();
                // 每段用原始比例参与堆叠，例如 100 / 1610，显示时才四舍五入为 6.2%。
                var shares = counts.Select(count => total > 0 ? (double)count / total : 0).ToArray();
                return new
                {
                    Category = category,
                    Direct = shares[0], DirectLabel = Percent(shares[0]),
                    MailAd = shares[1], MailAdLabel = Percent(shares[1]),
                    AffiliateAd = shares[2], AffiliateAdLabel = Percent(shares[2]),
                    VideoAd = shares[3], VideoAdLabel = Percent(shares[3]),
                    SearchEngine = shares[4], SearchEngineLabel = Percent(shares[4])
                };
            }).ToArray();
        }

        private static string Percent(double value)
        {
            var tenths = Math.Floor(value * 1000 + 0.5);
            return (tenths / 10).ToString("0.#", CultureInfo.InvariantCulture) + "%";
        }
    }
}
