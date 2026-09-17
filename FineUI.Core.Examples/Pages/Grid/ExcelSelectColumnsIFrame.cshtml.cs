using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class ExcelSelectColumnsIFrame : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SelectColumnsIFrame_btnSaveContinue_Click(object sender, EventArgs e)
        {
            // 关闭弹出窗体，然后执行父页面的JavaScript函数（exportToExcel）并传入参数
            ActiveWindow.HideExecuteScript(String.Format("exportToExcel({0});", new JArray(cblColumns.SelectedValueArray).ToString(Newtonsoft.Json.Formatting.None)));
        }

    }
}