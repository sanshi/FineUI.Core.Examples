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
    public partial class RowSelectCustomEventModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_RowSelect")
            {
                var param = e.EventArgumentsAsJObject;
                ShowNotify(String.Format("你 {4} 了第 {0} 行，行ID：{1}，姓名：{2}，列：{3}",
                            param.Value<int>("rowIndex") + 1,
                            param.Value<string>("rowId"),
                            param.Value<string>("rowText"),
                            param.Value<string>("columnText"),
                            param.Value<bool>("isDeselect") ? "取消选中" : "选中"));
            }
        }

    }
}