using System;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class ReorderModel : PageBase
    {
        protected void OpenFirst(object sender, EventArgs e)
        {
            TabStrip1.ActiveTabIndex = 0;
            labResult.Text = "后台第一项：" + TabStrip1.Tabs[0].Title;
        }

        protected void OpenHome(object sender, EventArgs e)
        {
            TabStrip1.ActiveTab = tabHome;
        }
    }
}
