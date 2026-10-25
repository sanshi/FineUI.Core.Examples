using System;
using System.Data;
using System.Text;

namespace FineUI.Core.Examples
{
    /// <summary>动态换列使用固定学号、统计编号和日期；往返切换读取同一份数据，不重新生成随机记录。</summary>
    public static class DynamicColumnsData
    {
        public static DataTable GetStudents()
        {
            DataTable table = DataSourceUtil.GetDataTable().DefaultView.ToTable(false,
                "Id", "Name", "Gender", "EntranceYear", "AtSchool", "Major", "Group", "LogTime", "Desc");
            foreach (DataRow row in table.Rows)
            {
                int id = (int)row["Id"];
                int daysAgo;
                if (id <= 104)
                {
                    daysAgo = 100;
                }
                else if (id == 105)
                {
                    daysAgo = 60;
                }
                else if (id <= 110)
                {
                    daysAgo = 50;
                }
                else
                {
                    daysAgo = 5;
                }
                row["LogTime"] = new DateTime(2020, 1, 1).AddDays(-daysAgo);
                var description = new StringBuilder($"{row["Name"]}，就读于{row["Major"]}，于 {row["EntranceYear"]} 年入学，是一名勤奋好学、积极向上的学生。");
                for (int paragraph = 0; paragraph < id % 4; paragraph++)
                {
                    description.Append("平日里热爱阅读与运动，乐于助人，积极参加各类社团活动与志愿服务，深受师生好评。");
                }
                row["Desc"] = description.ToString();
            }
            return table;
        }

        public static DataTable GetStatistics()
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Year", typeof(int));
            string[] fields = { "HZData1", "HZData2", "HLData1", "HLData2", "AHData1", "AHData2" };
            int[] factors = { 3, 5, 7, 11, 13, 17 };
            foreach (string field in fields)
            {
                table.Columns.Add(field, typeof(int));
            }
            table.Columns.Add("LogTime", typeof(DateTime));
            for (int index = 0; index < 10; index++)
            {
                DataRow row = table.NewRow();
                row["Id"] = 100 + index;
                row["Year"] = 2000 + index;
                for (int column = 0; column < fields.Length; column++)
                {
                    int factor = factors[column];
                    // 按行和字段计算稳定样本，例如第一行 HZData1 始终为 1291。
                    row[fields[column]] = 1000 + ((index * factor * 631 + factor * 97) % 8999);
                }
                row["LogTime"] = new DateTime(2000 + index, 9, 1);
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
