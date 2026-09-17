using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class NotifyModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Notify notify = new Notify();
            notify.Message = tbxMessage.Text;

            notify.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), ddlMessageBoxIcon.SelectedValue, true);
            notify.Target = (Target)Enum.Parse(typeof(Target), ddlTarget.SelectedValue, true);


            if (!String.IsNullOrEmpty(nbWidth.Text))
            {
                notify.Width = Convert.ToInt32(nbWidth.Text);
            }

            if (Convert.ToBoolean(cbxShowHeader.Checked))
            {
                notify.ShowHeader = true;

                if (!String.IsNullOrEmpty(tbxTitle.Text))
                {
                    notify.Title = tbxTitle.Text;
                }

                notify.EnableDrag = Convert.ToBoolean(cbxEnableDrag.Checked);
                notify.EnableClose = Convert.ToBoolean(cbxEnableClose.Checked);
            }
            else
            {
                notify.ShowHeader = false;
            }


            notify.DisplayMilliseconds = Convert.ToInt32(nbDisplayMilliseconds.Text);
            notify.DisplayProgress = Convert.ToBoolean(cbxDisplayProgress.Checked);

            notify.PositionX = (Position)Enum.Parse(typeof(Position), ddlPositionX.SelectedValue, true);
            notify.PositionY = (Position)Enum.Parse(typeof(Position), ddlPositionY.SelectedValue, true);

            notify.IsModal = Convert.ToBoolean(cbxIsModal.Checked);

            if (!String.IsNullOrEmpty(tbxBodyPadding.Text))
            {
                notify.BodyPadding = tbxBodyPadding.Text;
            }

            notify.MessageAlign = (TextAlign)Enum.Parse(typeof(TextAlign), ddlMessageAlign.SelectedValue, true);

            if (Convert.ToBoolean(cbxShowLoading.Checked))
            {
                notify.ShowLoading = true;
            }

            if (!String.IsNullOrEmpty(nbMinWidth.Text))
            {
                notify.MinWidth = Convert.ToInt32(nbMinWidth.Text);
            }

            if (!String.IsNullOrEmpty(nbMaxWidth.Text))
            {
                notify.MaxWidth = Convert.ToInt32(nbMaxWidth.Text);
            }

            if (!String.IsNullOrEmpty(tbxID.Text))
            {
                notify.ID = tbxID.Text;
            }


            notify.HideScript = "notifyHideCallback();";


            notify.Show();

        }

    }
}
