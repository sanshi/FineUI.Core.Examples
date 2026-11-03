using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TriggerBoxClearIconModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Alert.Show("文本框的输入值：" + TriggerBox1.Text);
        }

        protected void btnApplyVisibility_Click(object sender, EventArgs e)
        {
            bool automatic = cbxAutoClear.Checked;
            TriggerBox1.AutoShowClearIcon = automatic;
            TwinTriggerBox1.AutoShowClearIcon = automatic;
            cbxShowTrigger.Enabled = !automatic;
            cbxShowTrigger1.Enabled = !automatic;
            TwinTriggerBox1.ShowTrigger2 = cbxShowTrigger2.Checked;

            // 自动模式由文字接管第一按钮，不再同时写手动开关；关闭后才应用用户选择的手动值。
            if (!automatic)
            {
                TriggerBox1.ShowTrigger = cbxShowTrigger.Checked;
                TwinTriggerBox1.ShowTrigger = cbxShowTrigger.Checked;
                TwinTriggerBox1.ShowTrigger1 = cbxShowTrigger1.Checked;
            }
        }

        protected void btnReadVisibility_Click(object sender, EventArgs e)
        {
            labVisibility.Text = $"自动清除：{FormatBoolean(TriggerBox1.AutoShowClearIcon)}；"
                + $"单触发器手动显示：{FormatBoolean(TriggerBox1.ShowTrigger)}；"
                + $"双触发器手动显示：{FormatBoolean(TwinTriggerBox1.ShowTrigger)}；"
                + $"第一按钮手动显示：{FormatBoolean(TwinTriggerBox1.ShowTrigger1)}；"
                + $"第二按钮显示：{FormatBoolean(TwinTriggerBox1.ShowTrigger2)}；"
                + $"单触发器文本：{TriggerBox1.Text}；双触发器文本：{TwinTriggerBox1.Text}";
        }

        protected void btnFillText_Click(object sender, EventArgs e)
        {
            TriggerBox1.Text = "后台文字";
            TwinTriggerBox1.Text = "后台文字";
        }

        protected void btnClearText_Click(object sender, EventArgs e)
        {
            TriggerBox1.Text = String.Empty;
            TwinTriggerBox1.Text = String.Empty;
        }

        protected void TwinTriggerBox1_Trigger2Click(object sender, EventArgs e)
        {
            labTriggerResult.Text = "执行第二个触发按钮";
        }

        private static string FormatBoolean(bool value)
        {
            return value ? "是" : "否";
        }

    }
}
