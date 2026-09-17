using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class FormValidateModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (tbxUserName.Text == "admin")
            {
                tbxUserName.MarkInvalid(String.Format("{0} 是保留字，请另外选择！", tbxUserName.Text));

                //// 用户名验证失败，则重新聚焦到用户名
                //tbxUserName.Focus(true, 200);
            }
            else
            {
                ShowNotify(String.Format("用户名：{0} 密码：{1}", tbxUserName.Text, tbxPassword.Text));
            }
        }

    }
}