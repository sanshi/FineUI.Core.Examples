using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Button
{
    public partial class ButtonIconFontRoundCornerModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCustomIconFont_Click(object sender, EventArgs e)
        {
            if (btnCustomIconFont.IconFont == IconFont._VolumeUp)
            {
                btnCustomIconFont.IconFont = IconFont._VolumeDown;
            }
            else if (btnCustomIconFont.IconFont == IconFont._VolumeDown)
            {
                btnCustomIconFont.IconFont = IconFont._VolumeOff;
            }
            else
            {
                btnCustomIconFont.IconFont = IconFont._VolumeUp;
            }
        }
    }
}
