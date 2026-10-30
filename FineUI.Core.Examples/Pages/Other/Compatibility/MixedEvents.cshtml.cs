using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class MixedEventsModel : PageBase
    {
        public IActionResult OnPostAppend()
        {
            labHistory.Text += " → OnPost";
            UIHelper.Label("labImmediate").Text("旧事件已完成");
            return UIHelper.Result();
        }

        public void btnRazorForms_Click(object sender, EventArgs e)
        {
            labHistory.Text += " → RazorForms";
        }

        public IActionResult OnPostOpen()
        {
            Window1.Show();
            return UIHelper.Result();
        }

        public void btnCloseWindow_Click(object sender, EventArgs e)
        {
            Window1.Hide();
        }
    }
}
