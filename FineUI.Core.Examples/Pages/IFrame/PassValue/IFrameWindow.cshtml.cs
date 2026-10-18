using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.PassValue
{
    public partial class IFrameWindowModel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSheng.DataTextField = "Name";
                ddlSheng.DataValueField = "Code";
                ddlSheng.DataSource = ProvinceCityCountyData.Provinces;
                ddlSheng.DataBind();
            }
        }


        protected void ddlSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveWindow.WriteBackValue(ProvinceCityCountyData.ProvinceName(ddlSheng.SelectedValue));
            ActiveWindow.Hide();
        }
    }
}