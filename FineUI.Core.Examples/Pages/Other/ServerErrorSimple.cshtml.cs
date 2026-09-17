using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Other
{
    public partial class ServerErrorSimpleModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PageManager.Instance.SimpleError = true;
            }
        }


        
        protected void Button5_Click(object sender, EventArgs e)
        {
            throw new Exception("服务器异常错误！");
        }

    }
}