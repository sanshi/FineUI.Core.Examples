using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class RadioButtonModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void btnSelectSingleRadio_Click(object sender, EventArgs e)
        {
            rbtnSingleRadio.Checked = !rbtnSingleRadio.Checked;
        }

        protected void btnSelectSecondRadio_Click(object sender, EventArgs e)
        {
            String[] radios = new String[] { "rbtnFirst", "rbtnSecond", "rbtnThird" };

            for (int i = 0; i < radios.Length; i++)
            {
                if ((FineUI.Core.PageContext.FindControl(radios[i]) as RadioButton).Checked)
                {
                    int next = i + 1;
                    if (next >= radios.Length)
                    {
                        next = 0;
                    }
                    (FineUI.Core.PageContext.FindControl(radios[next]) as RadioButton).Checked = true;

                    break;
                }
            }
        }



        protected void rbtnAuto_CheckedChanged(object sender, EventArgs e)
        {
            string checkedValue = String.Empty;
            if (rbtnFirstAuto.Checked)
            {
                checkedValue = rbtnFirstAuto.Text;
            }
            else if (rbtnSecondAuto.Checked)
            {
                checkedValue = rbtnSecondAuto.Text;
            }
            else if (rbtnThirdAuto.Checked)
            {
                checkedValue = rbtnThirdAuto.Text;
            }

            ShowNotify("单选框选中项：" + checkedValue);
        }



    }
}