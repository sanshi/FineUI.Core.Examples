using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace FineUI.Core.Examples
{
    public class NewFilteredTable
    {
        public delegate bool FilterDataRowItemDelegate(object sourceObj, GridColumnFilteredItem filteredItem, string columnID);
        public FilterDataRowItemDelegate FilterDataRowItem
        {
            get;
            set;
        }

        // 示例使用的模拟数据是一次性返回的，因此我们需要新建一个 DataTable 来过滤返回的数据
        // 注：实际应用环境请不要这么做！！！（可以将过滤条件直接用于数据库检索）
        public DataTable GetFilteredTable(FineUI.Core.Grid gridInstance)
        {
            DataTable source = DataSourceUtil.GetDataTable();

            DataTable result = source.Clone();

            foreach (DataRow row in source.Rows)
            {
                bool filtered = true;
                foreach (GridColumn column in gridInstance.Columns)
                {
                    if (!CheckDataRow(row, column))
                    {
                        // 如果不满足某一列的过滤条件，则将当前行数据排除在外
                        // break用来终止循环，只要有一列的过滤条件不满足，则无需再检查其他列的过滤条件
                        filtered = false;
                        break;
                    }
                }

                if (filtered)
                {
                    result.Rows.Add(row.ItemArray);
                }
            }

            return result;
        }


        private bool CheckDataRow(DataRow row, GridColumn column)
        {
            var columnFilteredData = column.ColumnFilteredData;
            if (columnFilteredData == null || columnFilteredData.Items.Count == 0)
            {
                // 如果当前列没有过滤条件，则直接返回 true
                return true;
            }

            string columnID = columnFilteredData.ColumnID;
            object rowitemData = row[columnID];

            string matcher = columnFilteredData.Matcher;

            bool valid = false;
            if (matcher == "all")
            {
                valid = true;
            }
            foreach (var item in columnFilteredData.Items)
            {
                bool filterResult = false;
                if (FilterDataRowItem != null)
                {
                    filterResult = FilterDataRowItem(rowitemData, item, columnID);
                }

                if (filterResult)
                {
                    if (matcher == "any")
                    {
                        valid = true;
                        break;
                    }
                }
                else
                {
                    if (matcher == "all")
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }



    }

}
