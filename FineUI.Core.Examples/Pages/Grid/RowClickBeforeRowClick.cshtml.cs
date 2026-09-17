using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class RowClickBeforeRowClickModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Grid1_RowClick(object sender, GridRowEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];

            string message = String.Format("你单击了第 {0} 行，行ID：{1}，姓名：{2}", e.RowIndex + 1, keys[0], keys[1]);

            // 选中单元格所在的行
            if (Grid1.SelectedCell != null && Grid1.SelectedCell.Length > 0)
            {
                message += String.Format("，列：{0}", Grid1.SelectedCell[1]);
            }

            ShowNotify(message);
        }

    }
}