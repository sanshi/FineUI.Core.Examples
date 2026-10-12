using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Layout
{
    public partial class VBoxFormGridModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid2.DataSource = DataSourceUtil.GetDataTable();
                Grid2.DataBind();
            }
        }


        

    }
}