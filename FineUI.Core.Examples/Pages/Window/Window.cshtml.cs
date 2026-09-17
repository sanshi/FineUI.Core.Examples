using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Window
{
    public partial class WindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            Alert.Show("触发了窗体的关闭事件！");
        }

        protected void btnShowInServer_Click(object sender, EventArgs e)
        {
            Window1.Hidden = false;
        }


        protected void btnHideInServer_Click(object sender, EventArgs e)
        {
            Window1.Hidden = true;
        }

    }
}