using System;

namespace FineUI.Core.Examples.Pages.Other.LegacyScripts
{
    public partial class ListenersModel : LegacyScriptPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnName.Listeners.Add(new Listener("click", "onLegacyNameClick"));
                btnCall.Listeners.Add(new Listener("click", "showLegacyCallResult()"));
                btnSemicolon.Listeners.Add(new Listener("click", "showLegacyCallResult();"));
                btnArguments.Listeners.Add(new Listener("click", "showLegacyResult('带参数调用')"));
                btnStatements.Listeners.Add(new Listener("click", "var message = '多条语句'; showLegacyResult(message);"));
            }
        }
    }
}
