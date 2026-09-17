using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Form
{
    public partial class RadioButtonListImageItemModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataBind();
                LoadItemDataBound();
                LoadManualItems();
            }
        }

        // 示例用国家数据（Code 为国家代码，对应 ~/res/icon/flag_{code}.png；Name 为显示名称）
        private List<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country("cn", "中国"),
                new Country("us", "美国"),
                new Country("ru", "俄罗斯"),
                new Country("de", "德国")
            };
        }

        #region ① DataBind + DataTextRaw（自动绑定：整列文本按可信 HTML 输出）

        private void LoadDataBind()
        {
            // 后端预拼好每一项的可信 HTML（图标 + 名称），放到 Display 字段
            List<Country> list = new List<Country>();
            foreach (Country c in GetCountries())
            {
                // 只信任图标模板骨架 + 可信 URL；数据字段（名称）经 HtmlEncode 转义（与另两种方式完全一致）
                c.Display = String.Format("<img src=\"{0}\" style=\"vertical-align:middle;\" />&nbsp;{1}",
                    Url.Content("~/res/icon/flag_" + c.Code + ".png"), HttpUtility.HtmlEncode(c.Name));
                list.Add(c);
            }

            RadioButtonList1.DataValueField = "Code";
            RadioButtonList1.DataTextField = "Display";
            RadioButtonList1.DataTextRaw = true;      // 整列文本作为可信 HTML 原样输出（不转义）
            RadioButtonList1.DataSource = list;
            RadioButtonList1.DataBind();

            RadioButtonList1.SelectedValue = "cn";
        }

        #endregion

        #region ② OnItemDataBound（逐项事件回调；三种方式安全性等价）

        private void LoadItemDataBound()
        {
            RadioButtonList2.DataValueField = "Code";
            RadioButtonList2.DataTextField = "Name";
            RadioButtonList2.DataSource = GetCountries();
            RadioButtonList2.DataBind();

            RadioButtonList2.SelectedValue = "us";
        }

        protected void RadioButtonList2_ItemDataBound(object sender, RadioItemEventArgs e)
        {
            Country c = e.DataItem as Country;

            // 只信任图标模板骨架 + 可信 URL；数据字段（名称）经 HtmlEncode 转义，杜绝 XSS
            e.Item.TextRawHtml = new RawHtml("<img src=\"{0}\" style=\"vertical-align:middle;\" />&nbsp;{1}",
                Url.Content("~/res/icon/flag_" + c.Code + ".png"),
                HttpUtility.HtmlEncode(c.Name));
        }

        #endregion

        #region ③ 手工构建 Items（直接对 Items 赋值，TextRawHtml）

        private void LoadManualItems()
        {
            foreach (Country c in GetCountries())
            {
                RadioButtonList3.Items.Add(new RadioItem
                {
                    Value = c.Code,
                    // TextRawHtml 便捷写法：只信任图标模板，名称经 HtmlEncode 转义
                    TextRawHtml = new RawHtml("<img src=\"{0}\" style=\"vertical-align:middle;\" />&nbsp;{1}",
                        Url.Content("~/res/icon/flag_" + c.Code + ".png"),
                        HttpUtility.HtmlEncode(c.Name))
                });
            }

            RadioButtonList3.SelectedValue = "ru";
        }

        #endregion

        #region Country

        public class Country
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Display { get; set; }

            public Country(string code, string name)
            {
                Code = code;
                Name = name;
            }
        }

        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowNotify(String.Format("① {0}　② {1}　③ {2}",
                RadioButtonList1.SelectedValue, RadioButtonList2.SelectedValue, RadioButtonList3.SelectedValue));
        }

    }
}
