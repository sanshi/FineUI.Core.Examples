using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class SelectProvinceModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                ddlProvince.DataSource = DataSourceUtil.SHENG_JSON;
                ddlProvince.DataBind();
            }
        }
        

        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            ShowNotify("触发了 Window1 的关闭事件！");
        }

    }
}