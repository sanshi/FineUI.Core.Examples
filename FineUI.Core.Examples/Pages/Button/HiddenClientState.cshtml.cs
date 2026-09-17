using System;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class HiddenClientStateModel : BaseModel
    {
        protected void btnReadOnServer_Click(object sender, EventArgs e)
        {
            ShowNotify(TargetButton.Hidden ? "服务端状态：隐藏" : "服务端状态：显示");
        }
    }
}
