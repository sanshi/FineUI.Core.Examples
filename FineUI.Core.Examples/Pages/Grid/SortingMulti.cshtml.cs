using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class SortingMultiModel : BaseModel
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
            Grid1.DataSource = GetSortedDataTable(Grid1.SortFieldArray);
            Grid1.DataBind();


            labSortOrderTip.Text = GetSortTip();
        }


        private string GetSortFieldTip(string sortField, string sortDirection)
        {
            var sortFieldTitle = "";
            foreach (var column in Grid1.Columns)
            {
                if (column.SortField == sortField)
                {
                    sortFieldTitle = column.HeaderText;
                    break;
                }
            }
            return String.Format("{0}（{1}）", sortFieldTitle, sortDirection == "ASC" ? "升序" : "降序");
        }

        private string GetSortTip()
        {
            List<string> sortTips = new List<string>();

            string[] sortFields = Grid1.SortFieldArray;
            // 多列排序
            if (sortFields.Length > 0)
            {
                for (var i = 0; i < sortFields.Length; i += 2)
                {
                    var sortField = sortFields[i];
                    var sortDirection = sortFields[i + 1];

                    sortTips.Add(GetSortFieldTip(sortField, sortDirection));
                }
            }

            return String.Format("排序字段：{0}", String.Join("，", sortTips));
        }


        private DataTable GetSortedDataTable(string[] sortFields)
        {
            DataTable table = DataSourceUtil.GetDataTable();

            // 多列排序
            if (sortFields != null && sortFields.Length > 0)
            {
                List<string> sortItems = new List<string>();
                for (var i = 0; i < sortFields.Length; i += 2)
                {
                    sortItems.Add(String.Format("{0} {1}", sortFields[i], sortFields[i + 1]));
                }

                DataView view1 = table.DefaultView;
                view1.Sort = String.Join(", ", sortItems);

                return view1.ToTable();
            }
            else
            {
                return table;
            }
        }

        protected void Grid1_Sort(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}