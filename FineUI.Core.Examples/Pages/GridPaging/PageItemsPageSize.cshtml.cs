using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class PageItemsPageSizeModel : BaseModel
    {

        protected void btnServerPage_Click(object sender, EventArgs e)
        {
            new GridAjaxHelper(Grid1).LoadPageData(1);
        }

        protected void btnServerSort_Click(object sender, EventArgs e)
        {
            // 排序针对浏览器已有数据，随后重新切分首页；不重新查询或绑定数据源。
            var grid = new GridAjaxHelper(Grid1);
            grid.LoadSortData("EntranceYear", "DESC");
            grid.LoadPageData(0);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

    }
}
