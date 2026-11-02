using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ParametersModel : LegacyScriptPageBase
    {
        public IActionResult OnPostCapture(string note)
        {
            labResult.Text = "收到备注：" + note;
            return UIHelper.Result();
        }
    }
}
