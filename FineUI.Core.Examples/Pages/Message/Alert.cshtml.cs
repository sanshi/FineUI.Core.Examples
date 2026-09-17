using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class AlertModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Alert alert = new Alert();
            alert.Message = tbxMessage.Text;
            alert.Title = tbxTitle.Text;
            alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), rblMessageBoxIcon.SelectedValue, true);
            alert.Target = (Target)Enum.Parse(typeof(Target), rblTarget.SelectedValue, true);

            if (!String.IsNullOrEmpty(nbWidth.Text))
            {
                alert.Width = Convert.ToInt32(nbWidth.Text);
            }

            if (!String.IsNullOrEmpty(nbMinWidth.Text))
            {
                alert.MinWidth = Convert.ToInt32(nbMinWidth.Text);
            }

            if (!String.IsNullOrEmpty(nbMaxWidth.Text))
            {
                alert.MaxWidth = Convert.ToInt32(nbMaxWidth.Text);
            }

            if (!String.IsNullOrEmpty(tbxID.Text))
            {
                alert.ID = tbxID.Text;
            }

            if (!Convert.ToBoolean(cbxEnableClose.Checked))
            {
                alert.EnableClose = false;
            }

            alert.Show();
        }

    }
}