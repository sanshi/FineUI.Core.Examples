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
    
    public partial class DropDownListModel : BaseModel
    {
        public void OnGet()
        {
            var listItems = new List<ListItem>
            {
                new ListItem {
                    Text = "可选项1",
                    Value = "Value1"
                },
                new ListItem {
                    Text = "可选项2（不可选择）",
                    Value = "Value2",
                    EnableSelect = false
                },
                new ListItem {
                    Text = "可选项3（不可选择）",
                    Value = "Value3",
                    EnableSelect = false
                },
                new ListItem {
                    Text = "可选项4",
                    Value = "Value4"
                },
                new ListItem {
                    Text = "可选项5",
                    Value = "Value5"
                },
                new ListItem {
                    Text = "可选项6",
                    Value = "Value6"
                },
                new ListItem {
                    Text = "可选择项7",
                    Value = "Value7"
                },
                new ListItem {
                    Text = "可选择项8",
                    Value = "Value8"
                },
                new ListItem {
                    Text = "普通型1 < L > 1.5",
                    Value = "Value9"
                },
                new ListItem {
                    Text = "一个很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长很长的可选择项",
                    Value = "Value10"
                }
            };

            var model = new Models.DropDownListModel();
            model.DropDownList1 = "Value5";
            model.DropDownList1Items = listItems;


            model.DropDownList2 = new string[] { "Value1", "Value5" };
            model.DropDownList2Items = listItems;

            TheModel = model;
        }

        [BindProperty]
        public Models.DropDownListModel TheModel { get; set; }


        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                ShowNotify(new RawHtml("用户提交的数据：<br/><pre>{0}</pre>", EncodeJson(TheModel)));
            }
        }

    }
}