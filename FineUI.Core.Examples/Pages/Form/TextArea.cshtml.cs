using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TextAreaModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextArea1.Text = "北京时间2018年6月4日晚间，随着微软的一则公告，刚刚过完10岁的全球最大的社交编程及代码托管网站 GitHub正式卖身给微软。"
                    + "\r\n\r\n让业界吃惊的是，这一次微软的慷慨。"
                    + "\r\n\r\n按照上述公告，老牌科技公司微软最终以75亿美元的价格收购了GitHub，要知道GitHub在2015年时的最新估值只有20亿美元。"
                    + "\r\n\r\n事实上，在微软正式宣布收购之前，市场一直预测微软收购GitHub的交易价值是 50 亿美元。而50亿美元相比此前 20 亿美元的估值已经要高上一倍多。"
                    + "\r\n\r\n不过，上述大手笔收购也符合微软现任萨蒂亚·纳德拉（Satya Nadella）的行事风格。"
                    + "\r\n\r\n收购GitHub可以算是纳德拉坐上微软CEO位置4年来的第二大规模收购。而规模排名第一，那肯定得是两年前斥资262亿美元收购领英那场大动作。"
                    + "\r\n\r\n除了收购金额巨大之外，业界普遍关注点在于，最终买下开源社区GitHub的竟然是曾经相当封闭的微软。"
                    + "\r\n\r\n在这里不得不提的是，纳德拉的前任，也就是微软的第二任CEO史蒂夫·鲍尔默。鲍尔默此前一直反对将微软技术开源，他甚至将开源技术比喻为技术产权的癌症。"
                    + "\r\n\r\n后来，因为微软推出的开源软件托管平台 CodePlex 被 GitHub 后来者居上，两家的关系一度也比较紧张。"
                    + "\r\n\r\n但印度出生的“技术男”纳德拉出任微软的第三任CEO之后，微软开始表现出拥抱开源的姿态。"
                    + "\r\n\r\n公开信息显示，纳德拉2014年上任后，微软宣布开始在 GitHub 上建立账户。也是这一年的10月，微软宣布了.NET 的开源。而今年的2018 年 Build 大会上，微软还与 Github 达成进一步的合作，将微软的智能云 Azure 的能力开放出来。"
                    + "\r\n\r\n值得一提的是，曾经反对开源软件开发的微软，现在已经成了GitHub的最大贡献者之一。目前，微软在 GitHub上贡献了超过 1800 个存储库，连续三年保持第一。";
            }
        }

        
    }
}