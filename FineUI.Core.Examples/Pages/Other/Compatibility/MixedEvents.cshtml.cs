using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class MixedEventsModel : PageBase
    {
        public IActionResult OnPostAppend()
        {
            History.Text += " → OnPost";
            UIHelper.Label("Immediate").Text("旧事件已完成");
            return UIHelper.Result();
        }

        public void Direct_Click(object sender, EventArgs e)
        {
            History.Text += " → RazorForms";
        }

        public IActionResult OnPostOpen()
        {
            Dialog.Show();
            return UIHelper.Result();
        }

        public void Close_Click(object sender, EventArgs e)
        {
            Dialog.Hide();
        }
    }
}
