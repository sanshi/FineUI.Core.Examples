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
    
    public partial class CheckBoxListModel : PageBase
    {
        /// <summary>
        /// 固定选项每轮从服务器声明取得，不依赖只在 Page_Init 中装载的输入模型。
        /// </summary>
        public CheckItem[] GetDeclaredCheckItems()
        {
            return new List<CheckItem>
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
            }.ToArray();
        }

        /// <summary>
        /// 固定选项每轮从服务器声明取得，不依赖只在 Page_Init 中装载的输入模型。
        /// </summary>
        public RadioItem[] GetDeclaredRadioItems()
        {
            return new List<RadioItem>
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
                    Value = "Value3"
                }
            }.ToArray();
        }

        public void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var model = new Models.CheckBoxListModel();
                model.IsChecked = true;
                model.CheckBoxList1 = new string[] { "Value1", "Value3" };
                model.CheckBoxList1Items = GetDeclaredCheckItems().ToList();

                model.RadioButtonList1 = "Value1";
                model.RadioButton1Items = GetDeclaredRadioItems().ToList();

                model.Gender = 0;

                model.GenderType = GenderType.Woman;

                TheModel = model;
            }
        }

        [BindProperty]
        public Models.CheckBoxListModel TheModel { get; set; }


        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                // 单选列表写入模型；下拉列表另设 Name，直接从控件读取，允许两者选择不同。
                GenderType firstGender = TheModel.GenderType;
                string otherGender = TheModel_GenderType_2.Text;

                ShowNotify(new RawHtml(
                    "用户提交的数据：<br/><pre>{0}</pre>"
                        + "模型性别：{1}<br/>独立下拉列表 TheModel_GenderType_2：{2}",
                    EncodeJson(TheModel), EncodeJson(firstGender), EncodeJson(otherGender)));
            }
        }

    }
}
