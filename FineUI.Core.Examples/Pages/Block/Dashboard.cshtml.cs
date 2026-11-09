using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Block
{
    public partial class DashboardModel : PageBase
    {
        private static readonly string[] ChartColors =
        {
            "#c23531", "#2f4554", "#61a0a8", "#d48265", "#91c7ae", "#749f83",
            "#ca8622", "#bda29a", "#6e7074", "#546570", "#c4ccd3"
        };

        public void Page_Init(object sender, EventArgs e)
        {
            var pm = PageManager.Instance;
            pm.EnableWatermark = true;
            pm.WatermarkText = "I❤︎FineUI";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();

                // 卡片和页签共用同一份每日统计，切换页签时浏览量和用户数口径一致。
                var dailyChartRows = new[]
                {
                    new { Category = "02/01", Views = 800, Users = 400 },
                    new { Category = "02/02", Views = 1000, Users = 500 },
                    new { Category = "02/03", Views = 600, Users = 300 },
                    new { Category = "02/04", Views = 1200, Users = 900 },
                    new { Category = "02/05", Views = 600, Users = 300 },
                    new { Category = "02/06", Views = 500, Users = 600 },
                    new { Category = "02/07", Views = 800, Users = 600 },
                    new { Category = "02/08", Views = 500, Users = 300 },
                    new { Category = "02/09", Views = 1500, Users = 800 },
                    new { Category = "02/10", Views = 1000, Users = 1200 },
                    new { Category = "02/11", Views = 900, Users = 200 },
                    new { Category = "02/12", Views = 600, Users = 300 }
                };
                var dailyCharts = new[] { ChartCardViews, ChartCardTrend, ChartCardUsers, ChartTabViews, ChartTabUsers };
                foreach (var chart in dailyCharts)
                {
                    chart.Palette = CreateChartPalette();
                    chart.DataSource = dailyChartRows;
                    chart.DataBind();
                }

                ChartTrafficSources.Palette = CreateChartPalette();
                ChartTrafficSources.DataSource = new[]
                {
                    new { Category = "直接访问", Value = 335 },
                    new { Category = "邮件营销", Value = 310 },
                    new { Category = "联盟广告", Value = 234 },
                    new { Category = "视频广告", Value = 135 },
                    new { Category = "搜索引擎", Value = 1548 }
                };
                ChartTrafficSources.DataBind();
            }
        }

        private static string CreateChartPalette()
        {
            // 从旧版配色中随机选起点，保留颜色的相邻顺序。例如抽中 #61a0a8，就从它开始依次取色。
            int start = Random.Shared.Next(ChartColors.Length);
            return String.Join(",", ChartColors.Skip(start).Concat(ChartColors.Take(start)));
        }
    }
}