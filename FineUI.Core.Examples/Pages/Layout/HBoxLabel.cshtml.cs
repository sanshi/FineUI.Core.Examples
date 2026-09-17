using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Layout
{
    public partial class HBoxLabelModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = "<div class=\"zuckerberg\"><p><a ><b>马克·扎克伯格</b></a></p><p>马克·艾略特·扎克伯格（Mark Elliot Zuckerberg），美国社交网站Facebook的创办人，被人们冠以“第二盖茨”的美誉。哈佛大学计算机和心理学专业辍学生。据《福布斯》杂志保守估计，马克·扎克伯格拥有135亿美元身家，是2008年全球最年轻的巨富，也是历来全球最年轻的自行创业亿万富豪。</p><p>2014年2月10日，马克·扎克伯格及其华裔妻子普莉希拉·陈登上美国《慈善纪事报》2013年年度慈善排行榜榜首。</p></div>";
            }
        }




    }
}