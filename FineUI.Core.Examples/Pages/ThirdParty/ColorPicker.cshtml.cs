using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.ThirdParty
{
    public partial class ColorPickerModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("日期一：{0} 颜色值：{1}", DatePicker1.Text, tbxMyBox.Text));
        }

    }
}