using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class WindowModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void btnUpdateIFrameTitle_Click(object sender, EventArgs e)
        {
            // 回发时要一并声明需要保留的属性，窗体可能重建内部的 iframe。
            JObject attributes = (JObject)Window1.IFrameAttributes.DeepClone();
            attributes["title"] = "服务端已更新标题";
            attributes["sandbox"] = "allow-scripts allow-forms allow-modals";
            attributes["referrerpolicy"] = "no-referrer";
            Window1.IFrameAttributes = attributes;
        }


    }
}
