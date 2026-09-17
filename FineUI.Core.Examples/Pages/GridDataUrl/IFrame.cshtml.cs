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
    public partial class IFrameModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Alert.Show("触发了窗体的关闭事件！");
        }

    }
}