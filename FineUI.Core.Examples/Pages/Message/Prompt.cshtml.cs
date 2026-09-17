using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Message
{
    public partial class PromptModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Prompt prompt = new Prompt();
            prompt.Message = tbxMessage.Text;
            prompt.Title = tbxTitle.Text;
            prompt.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), rblMessageBoxIcon.SelectedValue, true);
            prompt.Target = (Target)Enum.Parse(typeof(Target), rblTarget.SelectedValue, true);

            if (Convert.ToBoolean(cbxIsMultiLine.Checked))
            {
                // 多行输入框
                prompt.MultiLine = true;

                if (!String.IsNullOrEmpty(nbMultiLineHeight.Text))
                {
                    prompt.MultiLineHeight = Convert.ToInt32(nbMultiLineHeight.Text);
                }
            }
            else
            {
                // 单行输入框，判断是否密码输入框
                if (Convert.ToBoolean(cbxIsPassword.Checked))
                {
                    prompt.TextMode = TextMode.Password;
                }
            }

            prompt.DefaultValue = tbxDefaultValue.Text;

            if (!String.IsNullOrEmpty(nbWidth.Text))
            {
                prompt.Width = Convert.ToInt32(nbWidth.Text);
            }

            if (!String.IsNullOrEmpty(nbMinWidth.Text))
            {
                prompt.MinWidth = Convert.ToInt32(nbMinWidth.Text);
            }

            if (!String.IsNullOrEmpty(nbMaxWidth.Text))
            {
                prompt.MaxWidth = Convert.ToInt32(nbMaxWidth.Text);
            }

            if (!String.IsNullOrEmpty(tbxID.Text))
            {
                prompt.ID = tbxID.Text;
            }

            if (Convert.ToBoolean(cbxRequired.Checked))
            {
                prompt.Required = true;
            }

            if (!Convert.ToBoolean(cbxEnableClose.Checked))
            {
                prompt.EnableClose = false;
            }

            prompt.OkScript = "promptOKCallback(arguments[0]);";

            prompt.Show();
        }

    }
}