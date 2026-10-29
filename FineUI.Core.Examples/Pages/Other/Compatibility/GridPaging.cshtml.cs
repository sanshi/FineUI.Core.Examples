using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FineUI.Core.Examples.Pages.Other.Compatibility
{
    public partial class GridPagingModel : PageBase
    {
        public object[] Rows { get; set; }

        public void OnGet()
        {
            Rows = LoadRows(0);
        }

        private static object[] LoadRows(int pageIndex)
        {
            return Enumerable.Range(pageIndex * 5 + 1, 5)
                .Select(id => new { Id = id, Name = $"客户{id:00}" }).ToArray();
        }

        public IActionResult OnPostPageChanged(string[] Grid1_fields, int Grid1_pageIndex)
        {
            int pageIndex = Math.Clamp(Grid1_pageIndex, 0, 2);
            UIHelper.Grid("Grid1").RecordCount(15);
            UIHelper.Grid("Grid1").DataSource(LoadRows(pageIndex), Grid1_fields);
            UIHelper.Label("Result").Text($"第 {pageIndex + 1} 页");
            return UIHelper.Result();
        }
    }
}
