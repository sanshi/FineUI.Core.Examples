using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.DataList
{
    public partial class DataListLinkToPanelGroupArrowModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }


        
        protected static readonly string DATALIST_ITEM_TEMPLATE_CHINA = "<table class=\"item-table\"><tr><td><img class=\"item-img\" src=\"{0}\"><div class=\"item-text\">{1}</div><div class=\"item-desc china\">{2}</div></td></tr></table>";


        private void LoadData()
        {
            DataList1.Items.Clear();
            DataTable source = DataSourceUtil.GetCountryTable();

            foreach (DataRow row in source.Rows)
            {
                DataListItem listItem = new DataListItem();

                string name = row["Name"].ToString();
                string groupName = row["Group"].ToString();

                listItem.Group = groupName;

                if (name == "中国")
                {
                    listItem.TextRawHtml = new RawHtml(DATALIST_ITEM_TEMPLATE_CHINA,
                        Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                        HttpUtility.HtmlEncode(row["Name"]),
                        HttpUtility.HtmlEncode(row["Desc"]));
                }
                else
                {
                    listItem.TextRawHtml = new RawHtml(DATALIST_ITEM_TEMPLATE,
                        Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                        HttpUtility.HtmlEncode(row["Name"]),
                        HttpUtility.HtmlEncode(row["Desc"]));

                    listItem.NavigateUrl = "#";

                    // 属于欧洲的子项，显示右侧箭头
                    if (groupName == "欧洲")
                    {
                        listItem.ShowArrow = true;
                    }

                }

                DataList1.Items.Add(listItem);
            }

        }


    }
}