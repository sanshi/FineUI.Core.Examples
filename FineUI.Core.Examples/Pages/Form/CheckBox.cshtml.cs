using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class CheckBoxModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }




        protected void btnSelectCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = !CheckBox1.Checked;
        }

        protected void btnDisableCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox1.Enabled = !CheckBox1.Enabled;
        }

        protected void btnChangeText_Click(object sender, EventArgs e)
        {
            CheckBox1.Text = String.Format("复选框（{0}）", DateTime.Now.ToLongTimeString());
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            labResult.Text = "复选框的状态：" + (CheckBox2.Checked ? "选中" : "未选中");
        }
    }
}
