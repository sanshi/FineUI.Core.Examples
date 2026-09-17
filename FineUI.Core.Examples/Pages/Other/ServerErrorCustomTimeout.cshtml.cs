using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class ServerErrorCustomTimeoutModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Button5_Click(object sender, EventArgs e)
        {
            // 故意延迟5秒，以便客户端AJAX请求超时
            System.Threading.Thread.Sleep(5000);

            throw new Exception("服务器异常错误！");
        }

    }
}