using Newtonsoft.Json.Linq;
using System.Data;


namespace FineUI.Core.Examples
{
    public class FilteredTable
    {
        public delegate bool FilterDataRowItemDelegate(object sourceObj, string itemOperator, JToken fieldValue, string column);
        public FilterDataRowItemDelegate FilterDataRowItem
        {
            get;
            set;
        }

        public delegate bool NewFilterDataRowItemDelegate(object sourceObj, string itemOperator, JToken fieldValue, string fieldText, string column);
        public NewFilterDataRowItemDelegate NewFilterDataRowItem
        {
            get;
            set;
        }

        // 表格过滤
        public DataTable GetFilteredTable(JArray filteredData)
        {
            // 示例使用的模拟数据是一次性返回的，因此我们需要新建一个 DataTable 来过滤返回的数据
            // 注：实际应用环境请不要这么做！！！（可以将过滤条件直接用于数据库检索）
            DataTable source = DataSourceUtil.GetDataTable();

            DataTable result = source.Clone();

            foreach (DataRow row in source.Rows)
            {
                bool filtered = true;
                foreach (JObject filteredObj in filteredData)
                {
                    if (!CheckDataRow(row, filteredObj))
                    {
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


        private bool CheckDataRow(DataRow row, JObject filteredObj)
        {
            string columnID = filteredObj.Value<string>("column");
            object rowitemData = row[columnID];

            bool multi = filteredObj.Value<bool>("multi");
            
            string matcher = filteredObj.Value<string>("matcher") ?? "any";
            JArray items = filteredObj.Value<JArray>("items");

            bool valid = false;
            if (matcher == "all")
            {
                valid = true;
            }
            foreach (JObject item in items)
            {
                string itemOperator = item.Value<string>("operator");
                JToken itemValue = item.Value<JToken>("value");
                string itemText = item.Value<string>("text");

                bool filterResult = false;
                if (FilterDataRowItem != null)
                {
                    filterResult = FilterDataRowItem(rowitemData, itemOperator, itemValue, columnID);
                }
                else if (NewFilterDataRowItem != null)
                {
                    filterResult = NewFilterDataRowItem(rowitemData, itemOperator, itemValue, itemText, columnID);
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
