using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class IFrameModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }



        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Alert.Show("触发了窗体的关闭事件！");
        }

        private void ShowEditWindow(object id, object name)
        {
            var iframeUrl = String.Format(Url.Content("~/Grid/IFrameWindow?id={0}&name={1}"), id, HttpUtility.UrlEncode(name.ToString()));
            new WindowAjaxHelper(Window1).Show(iframeUrl, "编辑 - " + name);
        }

        protected void Grid1_RowDoubleClick(object sender, GridRowEventArgs e)
        {
            object[] dataKeys = Grid1.DataKeys[e.RowIndex];

            ShowEditWindow(dataKeys[0], dataKeys[1]);
        }

    }
}