using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class TriggerBoxModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void btnCloseWindow_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
            TriggerBox1.Text = "弹出窗口被关闭了";
        }

        protected void btnApplyOptions_Click(object sender, EventArgs e)
        {
            TriggerBox1.EnableEdit = cbxEnableEdit.Checked;
            TwinTriggerBox1.EnableEdit = cbxEnableEdit.Checked;
            TwinTriggerBox1.EnableClickAction = cbxEnableClickAction.Checked;
        }

        protected void btnReadOptions_Click(object sender, EventArgs e)
        {
            labResult.Text = $"单触发器允许编辑：{FormatBoolean(TriggerBox1.EnableEdit)}；"
                + $"双触发器允许编辑：{FormatBoolean(TwinTriggerBox1.EnableEdit)}；"
                + $"双触发器点击触发：{FormatBoolean(TwinTriggerBox1.EnableClickAction)}；"
                + $"单触发器文本：{TriggerBox1.Text}；双触发器文本：{TwinTriggerBox1.Text}";
        }

        protected void btnToggleReadonly_Click(object sender, EventArgs e)
        {
            bool readOnly = !TriggerBox1.Readonly;
            TriggerBox1.Readonly = readOnly;
            TwinTriggerBox1.Readonly = readOnly;
        }

        protected void btnToggleEnabled_Click(object sender, EventArgs e)
        {
            bool enabled = !TriggerBox1.Enabled;
            TriggerBox1.Enabled = enabled;
            TwinTriggerBox1.Enabled = enabled;
        }

        protected void TwinTriggerBox1_Trigger1Click(object sender, EventArgs e)
        {
            labTriggerResult.Text = "执行第一个触发按钮";
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
