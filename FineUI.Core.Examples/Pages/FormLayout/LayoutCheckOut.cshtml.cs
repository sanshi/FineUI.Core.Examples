using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.FormLayout
{
    public partial class LayoutCheckOutModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void cbxSameAsContactAddress_CheckedChanged(object sender, EventArgs e)
        {
            tbxBillingAddress.Enabled = !cbxSameAsContactAddress.Checked;
            tbxBillingProvince.Enabled = !cbxSameAsContactAddress.Checked;
            tbxBillingCity.Enabled = !cbxSameAsContactAddress.Checked;
            tbxBillingPostCode.Enabled = !cbxSameAsContactAddress.Checked;

        }

    }
}