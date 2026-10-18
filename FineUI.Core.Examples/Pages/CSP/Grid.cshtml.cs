using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.CSP
{
    public partial class GridModel : BaseModel
    {
        public void Page_Init(object sender, EventArgs e)
        {
            // 首屏会从外部来源加载 underscore；全局严格 CSP 的其它设置保持不变。
            PageManager.Instance.CspScriptsAllowUrls = new[] { "cdn.jsdelivr.net", "unpkg.com" };
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }


        


    }
}
