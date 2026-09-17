using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class CheckBoxSwitchModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckBox5.SwitchOnTextRawHtml = new RawHtml("<i class=\"f-icon f-iconfont f-iconfont-check\"></i>");
                CheckBox5.SwitchOffTextRawHtml = new RawHtml("<i class=\"f-icon f-iconfont f-iconfont-close\"></i>");
            }
        }


        protected void btnSelectCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = !CheckBox1.Checked;
        }

        protected void btnDisableCheckBox_Click(object sender, EventArgs e)
        {
            CheckBox1.Enabled = !CheckBox1.Enabled;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            ShowNotify("复选框的状态：" + (CheckBox2.Checked ? "选中" : "未选中"));
        }

        protected void btnChangeText_Click(object sender, EventArgs e)
        {
            CheckBox1.Text = String.Format("复选框（{0}）", DateTime.Now.ToLongTimeString());
        }

    }
}
