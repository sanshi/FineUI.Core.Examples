using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class TabIndexChangedModel : PageBase
    {
        public IActionResult OnPostChanged(int activeIndex)
        {
            UIHelper.Label("labResult").Text($"当前标签：{activeIndex}");
            return UIHelper.Result();
        }

        public IActionResult OnPostNext(int activeIndex)
        {
            int nextIndex = (activeIndex + 1) % 3;
            UIHelper.TabStrip("TabStrip1").ActiveTabIndex(nextIndex);
            UIHelper.Label("labResult").Text($"当前标签：{nextIndex}");
            return UIHelper.Result();
        }
    }
}
