using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class GridNumberTrailingZeroModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 获取模拟表格（包含数字格式列的扩展表格，用于演示尾零省略效果）
        /// 注意：部分数据行刻意设置了整数或一位小数，以便直观对比尾零省略的效果
        /// </summary>
        /// <returns></returns>
        public DataTable GetExtendedDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));

            // 数字格式列（展示自定义格式的尾零省略效果）
            table.Columns.Add(new DataColumn("Salary", typeof(double)));           // 工资（千分位，尾零省略）
            table.Columns.Add(new DataColumn("GPA", typeof(double)));               // 绩点（定点数，尾零省略）
            table.Columns.Add(new DataColumn("AttendanceRate", typeof(double)));    // 出勤率（百分比，尾零省略）
            table.Columns.Add(new DataColumn("ResearchFund", typeof(double)));     // 科研经费（科学计数法，尾零省略）

            // 第1条数据
            DataRow row = table.NewRow();
            row[0] = 101;
            row[1] = "张萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 0;
            row[6] = "2000-09-01";
            row[7] = 12500.75;     // 工资
            row[8] = 3.75;          // 绩点
            row[9] = 0.9567;        // 出勤率
            row[10] = 2565000;     // 科研经费
            table.Rows.Add(row);

            // 第2条数据（Salary 末尾有一个零：9800.50 → 9,800.5）
            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 2000;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2001-09-01";
            row[7] = 9800.50;
            row[8] = 3.25;
            row[9] = 0.8923;
            row[10] = 1500000;
            table.Rows.Add(row);

            // 第3条数据
            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 2000;
            row[3] = true;
            row[4] = "化学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            row[7] = 15600.25;
            row[8] = 3.92;
            row[9] = 0.9876;
            row[10] = 3850000;
            table.Rows.Add(row);

            // 第4条数据（Salary/GPA 为整数：8500.00 → 8,500；2.00 → 2）
            row = table.NewRow();
            row[0] = 104;
            row[1] = "刘国";
            row[2] = 2020;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = 8500.00;
            row[8] = 2.00;
            row[9] = 0.75;
            row[10] = 8500000;
            table.Rows.Add(row);

            // 第5条数据
            row = table.NewRow();
            row[0] = 105;
            row[1] = "康颖颖";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            row[7] = 14200.60;
            row[8] = 3.68;
            row[9] = 0.9345;
            row[10] = 2950000;
            table.Rows.Add(row);

            // 第6条数据
            row = table.NewRow();
            row[0] = 106;
            row[1] = "彭博";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 1;
            row[6] = "2003-09-01";
            row[7] = 16800.90;
            row[8] = 3.45;
            row[9] = 0.9123;
            row[10] = 4520000;
            table.Rows.Add(row);

            // 第7条数据
            row = table.NewRow();
            row[0] = 107;
            row[1] = "黄婷婷";
            row[2] = 2008;
            row[3] = true;
            row[4] = "数学系";
            row[5] = 0;
            row[6] = "2000-09-01";
            row[7] = 13200.30;
            row[8] = 3.78;
            row[9] = 0.9678;
            row[10] = 3120000;
            table.Rows.Add(row);

            // 第8条数据（Salary/GPA 为整数：9200.00 → 9,200；2.00 → 2）
            row = table.NewRow();
            row[0] = 108;
            row[1] = "唐超";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = 9200.00;
            row[8] = 2.00;
            row[9] = 0.82;
            row[10] = 1200000;
            table.Rows.Add(row);

            // 第9条数据
            row = table.NewRow();
            row[0] = 109;
            row[1] = "杨婷婷";
            row[2] = 2020;
            row[3] = true;
            row[4] = "物理系";
            row[5] = 0;
            row[6] = "2003-09-01";
            row[7] = 11800.80;
            row[8] = 3.35;
            row[9] = 0.8956;
            row[10] = 2680000;
            table.Rows.Add(row);

            // 第10条数据
            row = table.NewRow();
            row[0] = 110;
            row[1] = "徐鹏";
            row[2] = 2020;
            row[3] = false;
            row[4] = "物理系";
            row[5] = 1;
            row[6] = "2020-09-01";
            row[7] = 10500.20;
            row[8] = 3.15;
            row[9] = 0.8679;
            row[10] = 1950000;
            table.Rows.Add(row);

            // 第11条数据
            row = table.NewRow();
            row[0] = 111;
            row[1] = "董国";
            row[2] = 2020;
            row[3] = true;
            row[4] = "自动化系";
            row[5] = 1;
            row[6] = "2006-09-01";
            row[7] = 15200.55;
            row[8] = 3.82;
            row[9] = 0.9456;
            row[10] = 3850000;
            table.Rows.Add(row);

            // 第12条数据
            row = table.NewRow();
            row[0] = 112;
            row[1] = "张三石";
            row[2] = 2012;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 1;
            row[6] = "2000-09-01";
            row[7] = 17800.40;
            row[8] = 3.28;
            row[9] = 0.9123;
            row[10] = 4850000;
            table.Rows.Add(row);

            return table;
        }



    }
}
