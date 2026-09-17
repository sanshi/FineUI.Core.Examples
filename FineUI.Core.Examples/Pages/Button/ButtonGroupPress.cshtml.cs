using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonGroupPressModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }


        protected void ButtonGroup3_PressChanged(object sender, EventArgs e)
        {
            ShowNotify(GetPressedButton("ButtonGroup3"));
        }

        protected void ButtonGroup4_PressChanged(object sender, EventArgs e)
        {
            ShowNotify(GetPressedButton("ButtonGroup4"));
        }

        protected void ButtonGroup5_PressChanged(object sender, EventArgs e)
        {
            ShowNotify(GetPressedButton("ButtonGroup5"));
        }

        private RawHtml GetPressedButton(string buttonGroupID)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("分组 {0} 中按下的按钮：", buttonGroupID);

            ButtonGroup theGroup = FineUI.Core.PageContext.FindControl(buttonGroupID) as ButtonGroup;

            if (theGroup != null)
            {
                sb.Append("<ul>");
                foreach (FineUI.Core.Button btn in theGroup.Items)
                {
                    if (btn.Pressed)
                    {
                        sb.AppendFormat("<li>{0}</li>", btn.Text);
                    }
                }
                sb.Append("</ul>");
            }

            return new RawHtml(sb.ToString());
        }

    }
}