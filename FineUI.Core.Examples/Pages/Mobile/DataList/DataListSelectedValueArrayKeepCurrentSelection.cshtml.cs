using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Mobile.DataList
{
    public partial class DataListSelectedValueArrayKeepCurrentSelectionModel : BaseMobileModel
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
            LoadData(false);
        }

        private void LoadData(bool reverse)
        {
            DataList1.Items.Clear();
            DataTable source = DataSourceUtil.GetCountryTable();

            DataRow[] rows = source.Select();
            if (reverse)
            {
                Array.Reverse(rows);
            }
            foreach (DataRow row in rows)
            {
                DataListItem listItem = new DataListItem();
                listItem.TextRawHtml = new RawHtml(DATALIST_SIMPLE_ITEM_TEMPLATE,
                    Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                    row["Name"]);
                listItem.Value = row["Id"].ToString();

                DataList1.Items.Add(listItem);
            }
        }


        // 服务端设置选择，替换当前可取消的选中项。
        protected void btnSetSelection_Click(object sender, EventArgs e)
        {
            DataList1.SelectedValueArray = new string[] { "fr", "us" };
        }

        // 服务端清空选择。
        protected void btnClearSelection_Click(object sender, EventArgs e)
        {
            DataList1.SelectedValueArray = new string[0];
        }

        // 倒序重绑同一批列表项，并选中美国。
        protected void btnRebindSelection_Click(object sender, EventArgs e)
        {
            LoadData(true);
            DataList1.SelectedValue = "us";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Alert.Show("选中项：" + String.Join(", ", DataList1.SelectedValueArray));
        }

    }
}
