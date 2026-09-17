using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormValidateBlurModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateForm(tbxUserName.Text))
            {
                ShowNotify(SimpleForm1);
            }
        }


        protected void tbxUserName_Blur(object sender, EventArgs e)
        {
            if (ValidateForm(tbxUserName.Text))
            {
                // 用户名失去焦点，并且用户名有效，则聚焦到下一个控件
                tbxPassword.Focus(true);
            }
        }
        

        private bool ValidateForm(string userName)
        {
            if (userName == "admin")
            {
                tbxUserName.MarkInvalid(String.Format("{0} 是保留字，请另外选择！", userName));
                return false;
            }
            else
            {
                tbxUserName.ClearInvalid();
                return true;
            }
        }
 

    }
}