using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class QueryParametersModel : PageBase
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostSave(int id, int dialog, string note, string source)
        {
            UIHelper.Label("labResult").Text($"编号：{id}；dialog：{dialog}；备注：{note}；来源：{source}");
            return UIHelper.Result();
        }
    }
}
