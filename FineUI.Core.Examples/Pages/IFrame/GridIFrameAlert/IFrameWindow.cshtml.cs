using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.GridIFrameAlert
{
    public partial class IFrameWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdateParentGrid_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 2. 先弹出提示对话框，再回发父窗体
            Alert.ShowInTop("保存成功！", String.Empty, MessageBoxIcon.Success, ActiveWindow.GetHidePostBackReference("参数 - " + DateTime.Now.Millisecond));

        }

    }
}