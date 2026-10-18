using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class ExcelSelectColumnsIFrame : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectColumnsIFrame_btnSaveContinue_Click(object sender, EventArgs e)
        {
            // 关闭弹出窗体后，把所选列数组交给原来打开窗体的页面，避免拼接执行脚本。
            ActiveWindow.HideInvokeFunction("exportToExcel", new JArray(cblColumns.SelectedValueArray));
        }

    }
}
