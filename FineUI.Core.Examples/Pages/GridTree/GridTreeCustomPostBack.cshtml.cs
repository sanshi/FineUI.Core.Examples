using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace FineUI.Core.Examples.Pages.GridTree
{
    public partial class GridTreeCustomPostBackModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Button1Click")
            {
                labResult.Text = String.Format("选中行状态：<pre>{0}</pre>", EncodeJson(e.EventArgumentsAsJArray));
            }
        }


    }
}