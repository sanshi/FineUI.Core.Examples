using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame.PassValue
{
    public partial class IFrameWindowModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ddlSheng_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(ddlSheng.SelectedValue) + ActiveWindow.GetHideReference());
        }
    }
}