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
    public partial class DataListMoreModel : BaseMobileModel
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
            BindList(0);
        }


        private void BindList(int dataIndex)
        {
            DataList1.Items.Clear();
            var items = GetSource(GetDataByIndex(dataIndex));
            foreach(var item in items)
            {
                DataList1.Items.Add(item);
            }
        }

        private DataListItem[] GetSource(DataTable source)
        {
            List<DataListItem> items = new List<DataListItem>();
            foreach (DataRow row in source.Rows)
            {
                DataListItem listItem = new DataListItem();
                listItem.TextRawHtml = new RawHtml(DATALIST_ITEM_TEMPLATE,
                    Url.Content("~/res/icon/flag_" + row["Image"] + ".png"),
                    HttpUtility.HtmlEncode(row["Name"]),
                    HttpUtility.HtmlEncode(row["Desc"]));

                items.Add(listItem);
            }
            return items.ToArray();
        }

        private DataTable GetDataByIndex(int dataIndex)
        {
            DataTable table = DataSourceUtil.GetCountryTable();
            if (dataIndex > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    var rowId = row["Id"].ToString();
                    var rowName = row["Name"].ToString();

                    row["Id"] = rowId + '_' + (dataIndex + 1).ToString();
                    row["Name"] = rowName + ' ' + (dataIndex + 1).ToString();
                }
            }
            return table;
        }

        private void LoadNextData(int dataIndex)
        {
            dataIndex++;
            if (dataIndex <= 4)
            {
                var dataSource = GetSource(GetDataByIndex(dataIndex));

                // AppendData: 追加数据
                DataList1.AppendData(dataSource);
                DataList1.Attributes["data-index"] = dataIndex.ToString();
            }
            if (dataIndex == 4)
            {
                btnMore.Enabled = false;
                btnMore.Text = "全部加载完毕";
            }
        }


        protected void btnMore_Click(object sender, EventArgs e)
        {
            var dataIndexStr = DataList1.Attributes.Value<string>("data-index");
            var dataIndex = String.IsNullOrEmpty(dataIndexStr) ? 0 : Convert.ToInt32(dataIndexStr);
            LoadNextData(dataIndex);
        }

    }
}