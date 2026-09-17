using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class ChangeConfirmFormModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void btnClosePostBack_Click(object sender, EventArgs e)
        {
            ShowNotify(SimpleForm1);

            // 保存数据后，清空面板内表单字段的改变状态
            SimpleForm1.ClearDirty();
        }

    }
}