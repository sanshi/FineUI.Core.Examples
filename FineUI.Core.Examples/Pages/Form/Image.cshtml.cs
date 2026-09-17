using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class ImageModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Image3.ImageWidth == 32)
            {
                Image3.ImageWidth = 64;
                Image3.ImageHeight = 64;
            }
            else
            {
                Image3.ImageWidth = 32;
                Image3.ImageHeight = 32;
            }
        }

    }
}