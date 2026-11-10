using System;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Chart.Pie
{
    public partial class ScrollableLegendModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChartPieScrollableLegend.DataSource = GetRows();
                ChartPieScrollableLegend.DataBind();
            }
        }

        private static object GetRows()
        {
            const string surnames = "赵钱孙李周吴郑王冯陈褚卫蒋沈韩杨朱秦尤许何吕施张孔曹严华金魏陶姜戚谢邹喻柏水窦章云苏潘葛奚范彭郎鲁韦昌马苗凤花方俞任袁柳酆鲍史唐费廉岑薛雷贺倪汤滕殷罗毕郝邬安常乐于时傅皮卞齐康伍余元卜顾孟平黄和穆萧尹姚邵湛汪祁毛禹狄米贝明臧计伏成戴谈宋茅庞熊纪舒屈项祝董梁杜阮蓝闵席季麻强贾路娄危";
            var random = new Random();
            return Enumerable.Range(0, 50).Select(_ =>
            {
                // 沿用官网两种姓名格式，例如「赵王吴」或「赵钱孙李·周吴」。
                var name = random.NextDouble() > 0.65
                    ? MakeNamePart(random, surnames, 4, 1) + "·" + MakeNamePart(random, surnames, 3, 0)
                    : MakeNamePart(random, surnames, 2, 1);
                return new { Category = name, Value = (int)Math.Floor(random.NextDouble() * 100000 + 0.5) };
            }).ToArray();
        }

        private static string MakeNamePart(Random random, string surnames, int lengthRange, int minimumLength)
        {
            var letters = new char[(int)Math.Ceiling(random.NextDouble() * lengthRange + minimumLength)];
            for (int index = 0; index < letters.Length; index++)
            {
                letters[index] = surnames[random.Next(surnames.Length)];
            }
            return new string(letters);
        }
    }
}
