using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.FormTable
{
    public partial class TableStyleFormModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmitForm1_Click(object sender, EventArgs e)
        {
            ShowNotify(Form1);
        }

        protected void btnSubmitForm2_Click(object sender, EventArgs e)
        {
            ShowNotify(Form2);
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            ShowNotify(Form1, Form2);
        }
        

    }
}