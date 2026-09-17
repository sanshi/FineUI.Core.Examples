using System;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckFieldPostBackModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_CheckFieldChanged")
            {
                var param = e.EventArgumentsAsJObject;
                ShowNotify(String.Format("你点击了的行ID：{0}，姓名：{1}，是否在校：{2}",
                    param.Value<string>("rowId"),
                    param.Value<string>("rowText"),
                    param.Value<bool>("isChecked") ? "是" : "否"));
            }
        }

    }
}
