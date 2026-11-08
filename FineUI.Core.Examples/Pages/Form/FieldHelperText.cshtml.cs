using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FieldHelperTextModel : PageBase
    {
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            tbxEmail.HelperText = "请填写部门公共邮箱，便于同事接收后续通知。";
        }

        protected void btnClearHelper_Click(object sender, EventArgs e)
        {
            tbxEmail.HelperText = null;
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            labResult.Text = tbxEmail.HelperText;
        }
    }
}
