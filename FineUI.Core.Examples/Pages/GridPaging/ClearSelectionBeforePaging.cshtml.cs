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
            new GridAjaxHelper(Grid1).SelectedCell("106", "Name");
        }

        protected void btnClearSelections_Click(object sender, EventArgs e)
        {
            FineUI.Core.PageContext.InvokeFunction("clearGridSelections");
        }

        protected void btnReadSelection_Click(object sender, EventArgs e)
        {
            var cell = Grid1.SelectedCell;
            var cellText = cell == null || cell.Length == 0 ? "空" : string.Join(",", cell);
            labServerSelection.Text = "选中行数：" + Grid1.SelectedRowIDArray.Length + "；单元格：" + cellText;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 首次加载才取数；回发重跑模板只取控件声明，保留浏览器已有数据。
                Grid1.DataSource = DataSourceUtil.GetDataTable2();
                Grid1.DataBind();
            }
        }


    }
}
