using System;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class LongPrecisionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable table = GetSimpleDataTable();

                // 表格一：使用默认配置，数据绑定时长整型的 Id 自动转为字符串下发，客户端原样显示
                Grid1.DataSource = table;
                Grid1.DataBind();

                // 表格二：显式关闭转换，长整型的 Id 以 JSON 数字下发；超过 2^53 - 1 的值会被 JavaScript 舍入，
                // 页面上看到的就是被改写后的数字（21956392701267968 显示为 21956392701267970）
                Grid2.DataSource = table;
                Grid2.DataBind();
            }
        }

        /// <summary>
        /// 构造演示数据：第一行的 Id 是超过 JavaScript 安全整数范围的 17 位长整型
        /// </summary>
        public static DataTable GetSimpleDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(long)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));


            DataRow row = table.NewRow();
            row[0] = 21956392701267968;
            row[1] = "张萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "材料科学与工程系";
            row[5] = 0;
            row[6] = "2000-09-01";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "陈飞";
            row[2] = 2000;
            row[3] = false;
            row[4] = "化学系";
            row[5] = 1;
            row[6] = "2001-09-01";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "董婷婷";
            row[2] = 2000;
            row[3] = true;
            row[4] = "化学系";
            row[5] = 0;
            row[6] = "2008-09-01";
            table.Rows.Add(row);

            return table;
        }
    }
}
