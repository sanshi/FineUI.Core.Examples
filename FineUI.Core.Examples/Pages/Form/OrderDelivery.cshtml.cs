using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class OrderDeliveryModel : PageBase
    {
        protected void ddlDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool needsAddress = ddlDelivery.SelectedValue == "courier";
            tbxAddress.Required = needsAddress;
            tbxAddress.ShowRedStar = needsAddress;
            labResult.Hidden = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            labResult.Hidden = false;
            if (ddlDelivery.SelectedValue == "courier")
            {
                string address = tbxAddress.Text?.Trim();
                labResult.Text = string.IsNullOrEmpty(address)
                    ? "请填写收货地址"
                    : $"订单已提交：快递配送，收货地址：{address}";
                return;
            }

            labResult.Text = "订单已提交：门店自提";
        }
    }
}
