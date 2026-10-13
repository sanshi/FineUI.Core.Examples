using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class IFrameWindowModel : BaseThirdPartyEditorModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 2. 关闭本窗体（触发窗体的关闭事件）
            ActiveWindow.HidePostBack();
        }

        protected void btnSaveHideRefresh_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 2. 隐藏本窗体，然后刷新父窗体
            ActiveWindow.HideRefresh();
        }

        protected void btnSaveCloseTab_Click(object sender, EventArgs e)
        {
            // 1. 这里放置保存窗体中数据的逻辑

            // 2. 等本窗体隐藏完成后，调用父页函数关闭当前激活的选项卡。
            ActiveWindow.HideCallParentFunction("closeOwnerTab");
        }


    }
}