using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridDataUrl
{
    public partial class ChangeDataUrlModel : BaseModel
    {
        protected void btnReload_Click(object sender, EventArgs e)
        {
            // 不传地址，沿用客户端当前地址；避免覆盖页面脚本已经切换的数据源。
            string gridId = Newtonsoft.Json.JsonConvert.SerializeObject(Grid1.ID);
            FineUI.Core.PageContext.RegisterStartupScript("F(" + gridId + ").loadDataUrl();");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var sourceKey = Grid1.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "source2")
            {
                Grid1.Attributes["data-source-key"] = "source1";
                Grid1.DataUrl = Url.Content("~/GridDataUrl/GridDataUrlData");
            }
            else
            {
                Grid1.Attributes["data-source-key"] = "source2";
                Grid1.DataUrl = Url.Content("~/GridDataUrl/GridDataUrlData?data2=true");
            }
        }
        
        protected void Button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}
