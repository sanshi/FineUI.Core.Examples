using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Layout
{
    public partial class RegionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            string newtitle = String.Format("左侧面板（有提示信息） - 更新时间：{0}", DateTime.Now.ToLongTimeString());
            panelLeftRegion.Title = newtitle;
            panelLeftRegion.TitleToolTip = newtitle;
        }


        protected void btnHideBottomRegion_Click(object sender, EventArgs e)
        {
            panelBottomRegion.Hidden = !panelBottomRegion.Hidden;
        }

    }
}