using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Web;


namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class MultiActionsColumnMenuButtonModel : BaseModel
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


        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            JObject param;
            if (e.EventName == "Grid1_CustomDelete")
            {
                param = e.EventArgumentsAsJObject;

                ShowNotify(String.Format("你点击了第 {0} 行的删除按钮，行ID：{1}，姓名：{2}",
                            param.Value<int>("rowIndex") + 1,
                            param.Value<string>("rowId"),
                            param.Value<string>("rowText")));
            }
            else if (e.EventName == "Grid1_CustomEdit")
            {
                param = e.EventArgumentsAsJObject;

                ShowNotify(String.Format("你点击了第 {0} 行的菜单项 {3}，行ID：{1}，姓名：{2}",
                             param.Value<int>("rowIndex") + 1,
                             param.Value<string>("rowId"),
                             param.Value<string>("rowText"),
                             param.Value<string>("actionName")));
            }
        }


        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Alert.Show("触发了窗体的关闭事件！");
        }


    }
}