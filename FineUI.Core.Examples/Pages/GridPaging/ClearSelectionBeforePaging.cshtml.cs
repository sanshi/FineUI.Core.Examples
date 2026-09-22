using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class ClearSelectionBeforePagingModel : BaseModel
    {
        protected void btnSelectCell_Click(object sender, EventArgs e)
        {
            // 106 行在第二页；选择当前客户端渲染页中的单元格。
            FineUI.Core.PageContext.RegisterStartupScript(Grid1.GetSelectCellReference("106", "Name"));
        }

        protected void btnClearSelections_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.RegisterStartupScript(Grid1.GetClearSelectionsReference());
        }

        protected void btnReadSelection_Click(object sender, EventArgs e)
        {
            var cell = Grid1.SelectedCell;
            var cellText = cell == null || cell.Length == 0 ? "空" : string.Join(",", cell);
            labServerSelection.Text = "选中行数：" + Grid1.SelectedRowIDArray.Length + "；单元格：" + cellText;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


    }
}
