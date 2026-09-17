using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class NumberBoxRateModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            int currentNum = Convert.ToInt32(NumberBox12.Text);

            currentNum++;
            if (currentNum > 5)
            {
                currentNum = 0;
            }

            NumberBox12.Text = currentNum.ToString();
        }

    }
}