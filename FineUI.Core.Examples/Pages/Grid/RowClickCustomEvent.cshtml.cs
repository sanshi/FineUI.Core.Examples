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
    public partial class RowClickCustomEventModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_RowClick")
            {
                var param = e.EventArgumentsAsJObject;
                ShowNotify(String.Format("你点击了第 {0} 行，行ID：{1}，姓名：{2}，列：{3}",
                    param.Value<int>("rowIndex") + 1,
                    param.Value<string>("rowId"),
                    param.Value<string>("rowText"),
                    param.Value<string>("columnText")));
            }
        }

    }
}