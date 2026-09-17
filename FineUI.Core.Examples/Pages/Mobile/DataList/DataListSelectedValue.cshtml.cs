using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.DataList
{
    public partial class DataListSelectedValueModel : BaseMobileModel
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
            DataList1.Items.Clear();
            DataTable source = DataSourceUtil.GetCountryTable();

            foreach (DataRow row in source.Rows)
            {
                DataListItem listItem = new DataListItem();
                listItem.TextRawHtml = new RawHtml(DATALIST_SIMPLE_ITEM_TEMPLATE,
                    Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                    row["Name"]);
                listItem.Value = row["Id"].ToString();

                DataList1.Items.Add(listItem);
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Alert.Show("选中项：" + DataList1.SelectedValue);
        }

    }
}