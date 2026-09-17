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
    public partial class ChangeDataUrlDatabasePagingModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                Grid1.DataUrl = Url.Content("~/GridDataUrl/PagingDatabaseData");
            }
            else
            {
                Grid1.Attributes["data-source-key"] = "source2";
                Grid1.DataUrl = Url.Content("~/GridDataUrl/PagingDatabaseData?data2=true");
            }

            // 重置为第一页
            Grid1.PageIndex = 0;
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}