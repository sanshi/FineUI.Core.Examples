using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridInput
{
    public partial class SelectRowUpDownModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName != "GetInputs")
            {
                return;
            }

            var inputs = e.EventArgumentsAsJArray;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=\"result\"><tr><th>ID</th><th>姓名</th><th>用户输入值</th></tr>");

            foreach (JArray item in inputs)
            {
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                    item[0], item[1], item[2]);
            }

            sb.Append("</table>");

            ShowNotify(new RawHtml(sb.ToString()), MessageBoxIcon.None);
        }

    }
}