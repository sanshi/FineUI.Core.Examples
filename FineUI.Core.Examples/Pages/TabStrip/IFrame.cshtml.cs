using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.TabStrip
{
    public partial class IFrameModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            var sourceKey = Tab2.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "source2")
            {
                Tab2.Attributes["data-source-key"] = "source1";
                Tab2.IFrameUrl = Url.Content("~/Panel/Group");
            }
            else
            {
                Tab2.Attributes["data-source-key"] = "source2";
                Tab2.IFrameUrl = Url.Content("~/Panel/Tools");
            }
        }

    }
}