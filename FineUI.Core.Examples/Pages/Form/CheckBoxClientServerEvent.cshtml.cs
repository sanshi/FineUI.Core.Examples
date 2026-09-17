using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class CheckBoxClientServerEventModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            labResult.Text = "【服务端】复选框1的状态：" + (CheckBox1.Checked ? "选中" : "未选中");
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            labResult.Text = "【服务端】复选框2的状态：" + (CheckBox2.Checked ? "选中" : "未选中");
        }


    }
}
