using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FineUI.Core.Examples.Pages.DataModel.Models;

namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class CheckBoxListModel : BaseModel
    {
        public void OnGet()
        {
            var model = new Models.CheckBoxListModel();
            model.IsChecked = true;
            model.CheckBoxList1 = new string[] { "Value1", "Value3" };
            model.CheckBoxList1Items = new List<CheckItem>
            {
                new CheckItem {
                    Text = "可选项 1",
                    Value = "Value1"
                },
                new CheckItem {
                    Text = "可选项 2",
                    Value = "Value2"
                },
                new CheckItem {
                    Text = "可选项 3",
                    Value = "Value3"
                }
            };

            model.RadioButtonList1 = "Value1";
            model.RadioButton1Items = new List<RadioItem>
            {
                new RadioItem {
                    Text = "可选项 1",
                    Value = "Value1"
                },
                new RadioItem {
                    Text = "可选项 2",
                    Value = "Value2"
                },
                new RadioItem {
                    Text = "可选项 3",
                    Value = "Value2"
                }
            };

            model.Gender = 0;

            model.GenderType = GenderType.Woman;

            TheModel = model;
        }

        [BindProperty]
        public Models.CheckBoxListModel TheModel { get; set; }


        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                ShowNotify(new RawHtml("用户提交的数据：<br/><pre>{0}</pre>", EncodeJson(TheModel)));
            }
        }

    }
}