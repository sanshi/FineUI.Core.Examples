using Microsoft.AspNetCore.Mvc;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class GridSelectionModel : PageBase
    {
        public object[] Rows { get; set; }

        public void OnGet()
        {
            Rows = new object[]
            {
                new { Id = "101", Name = "客户甲" },
                new { Id = "102", Name = "客户乙" },
                new { Id = "103", Name = "客户丙" }
            };
        }

        public IActionResult OnPostReadSelection(string[] selectedIds)
        {
            UIHelper.Label("labResult").Text("选中编号：" + string.Join("、", selectedIds));
            return UIHelper.Result();
        }
    }
}
