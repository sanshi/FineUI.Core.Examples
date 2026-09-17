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
    public partial class DataListDataBindModel : BaseMobileModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }



        private void LoadData()
        {
            AutoBindGrid();
        }



        private void BindListToDataTable(DataTable source)
        {
            DataList1.Items.Clear();
            foreach (DataRow row in source.Rows)
            {
                DataListItem listItem = new DataListItem();
                listItem.TextRawHtml = new RawHtml(DATALIST_ITEM_TEMPLATE,
                    Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                    HttpUtility.HtmlEncode(row["Name"]),
                    HttpUtility.HtmlEncode(row["Desc"]));

                DataList1.Items.Add(listItem);
            }
        }

        private void BindList()
        {
            BindListToDataTable(DataSourceUtil.GetCountryTable());
        }

        private void BindList2()
        {
            BindListToDataTable(DataSourceUtil.GetCountryTable2());
        }

        private void AutoBindGrid()
        {
            var sourceKey = DataList1.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "source2")
            {
                BindList();
                sourceKey = "source1";
            }
            else
            {
                BindList2();
                sourceKey = "source2";
            }

            DataList1.Attributes["data-source-key"] = sourceKey;
        }

        protected void btnReDataBind_Click(object sender, EventArgs e)
        {
            AutoBindGrid();
        }

    }
}