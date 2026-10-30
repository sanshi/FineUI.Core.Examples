using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class SelectionChangedModel : PageBase
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostEnabledChanged(bool enabled)
        {
            UIHelper.Label("labResult").Text(enabled ? "已接收通知" : "已关闭通知");
            return UIHelper.Result();
        }

        public IActionResult OnPostDepartmentChanged(string department)
        {
            string name = department == "sales" ? "销售部" : "研发部";
            UIHelper.Label("labResult").Text($"当前部门：{name}");
            return UIHelper.Result();
        }
    }
}
