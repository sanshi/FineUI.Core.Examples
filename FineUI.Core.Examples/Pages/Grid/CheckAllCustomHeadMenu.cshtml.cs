using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckAllCustomHeadMenuModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();

                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }


        private void LoadData()
        {
            Grid1.CheckBoxSelectHeaderTextRawHtml = new RawHtml("<i class=\"f-icon f-iconfont f-grid-checkbox f-checkbox\"></i><i class=\"f-icon f-iconfont f-iconfont-arrow-down custom-arrow-down\"></i>");
        }

        
        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.SelectedRowIDArray = new string[] { "102", "106", "108" };
        }


    }
}
