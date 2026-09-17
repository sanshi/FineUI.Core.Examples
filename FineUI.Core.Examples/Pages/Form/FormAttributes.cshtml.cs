using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormAttributesModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnChangeTip1_Click(object sender, EventArgs e)
        {
            Label1.ToolTip = "改变后的提示信息（ToolTip）";
        }

        protected void btnChangeTip2_Click(object sender, EventArgs e)
        {
            Label2.Attributes["data-qtip"] = "改变后的提示信息（Attributes）";
        }

    }
}