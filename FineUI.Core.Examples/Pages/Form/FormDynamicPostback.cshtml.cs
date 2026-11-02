using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormDynamicPostbackModel : PageBase
    {
        protected FineUI.Core.TextBox tbxRemark;
        protected FineUI.Core.Button btnConfirm;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // 再次点击时已有控件会保留，避免重复创建相同 ID。
            if (tbxRemark != null)
            {
                labResult.Text = "控件已存在，请输入备注后提交";
                return;
            }

            tbxRemark = new FineUI.Core.TextBox
            {
                ID = "tbxRemark",
                Label = "备注",
                EmptyText = "请输入备注",
                Required = true,
                ShowRedStar = true
            };
            btnConfirm = new FineUI.Core.Button { ID = "btnConfirm", Text = "提交新增备注" };
            btnConfirm.Events.Add(new Event("click", "btnConfirm_Click"));
            Panel1.Items.Add(tbxRemark);
            Panel1.Items.Add(btnConfirm);
            labResult.Text = "已追加控件，请输入备注后提交";
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            labResult.Text = String.IsNullOrWhiteSpace(tbxRemark.Text)
                ? "请填写备注"
                : "已提交：" + tbxRemark.Text;
        }
    }
}
