using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridDataUrl
{
    public partial class RowDoubleClickModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Grid1_RowDblClick(object sender, GridRowEventArgs e)
        {
            //ShowNotify(String.Format("你双击了第 {0} 行，行ID：{1}，姓名：{2}", rowIndex + 1, rowId, rowText));
            object[] keys = Grid1.DataKeys[e.RowIndex];
            ShowNotify(String.Format("你双击了第 {0} 行，行ID：{1}，姓名：{2}", e.RowIndex + 1, keys[0], keys[1]));
        }

    }
}