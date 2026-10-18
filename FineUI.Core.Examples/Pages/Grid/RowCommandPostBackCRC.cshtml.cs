using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class RowCommandPostBackCRCModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Grid1.DataSource = DataSourceUtil.GetDataTable();
                Grid1.DataBind();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_RowCommand")
            {
                var param = e.EventArgumentsAsJObject;
                ShowNotify(String.Format("你点击了第 {0} 行，第 {1} 列，行ID：{2}，姓名：{3}",
                            param.Value<int>("rowIndex") + 1,
                            param.Value<int>("columnIndex") + 1,
                            param.Value<string>("rowId"),
                            param.Value<string>("rowText")));
            }
        }

    }
}