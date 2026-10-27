using System;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class TemplateDeclarationsModel : PageBase
    {
        protected void btnRead_Click(object sender, EventArgs e)
        {
            ShowProperties();
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            tbxUserName.Label = "登录账号";
            tbxUserName.EmptyText = "请输入登录账号";
            tbxUserName.MaxLength = 30;
            ShowProperties();
        }

        private void ShowProperties()
        {
            // 标签、提示和长度限制在模板中声明；回发时可与用户当前输入一起读取。
            labResult.Text = String.Format(
                "当前输入：{0}\nLabel：{1}\nEmptyText：{2}\nMaxLength：{3}",
                tbxUserName.Text,
                tbxUserName.Label,
                tbxUserName.EmptyText,
                tbxUserName.MaxLength);
        }
    }
}
