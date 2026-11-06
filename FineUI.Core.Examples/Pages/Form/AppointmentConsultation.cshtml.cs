using System;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class AppointmentConsultationModel : PageBase
    {
        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            tpAppointment.Increment = ddlService.SelectedValue == "extended" ? 30 : 15;
            labResult.Hidden = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string service = ddlService.SelectedValue;
            string time = tpAppointment.Text;
            labResult.Hidden = false;

            // 两种咨询都能约整点和半点，09:15、09:45 只开放给常规咨询。
            bool sharedSlot = time is "09:00" or "09:30" or "10:00";
            bool standardOnlySlot = time is "09:15" or "09:45";
            bool available = service switch
            {
                "standard" => sharedSlot || standardOnlySlot,
                "extended" => sharedSlot,
                _ => false
            };
            if (!available)
            {
                labResult.Text = "请选择当前咨询项目可预约的开始时间。";
                return;
            }

            labResult.Text = $"预约已提交：{ddlService.Text}，开始时间：{time}";
        }
    }
}
