using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class DateParseStringModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Grid1.DataSource = GetDataTable();
                Grid1.DataBind();
            }
        }


        

        /// <summary>
        /// 获取模拟表格
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(String)));
            table.Columns.Add(new DataColumn("EntranceYear", typeof(int)));
            table.Columns.Add(new DataColumn("AtSchool", typeof(bool)));
            table.Columns.Add(new DataColumn("Major", typeof(String)));
            table.Columns.Add(new DataColumn("Group", typeof(int)));
            table.Columns.Add(new DataColumn("Gender", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));
            table.Columns.Add(new DataColumn("EntranceDate", typeof(String)));


            DataRow row = null;

            row = table.NewRow();
            row[0] = 101;
            row[1] = "陈萍萍";
            row[2] = 2000;
            row[3] = true;
            row[4] = "计算机应用技术";
            row[5] = 1;
            row[6] = 0;
            row[7] = DateTime.Now.AddDays(-100);
            row[8] = "09/01/2000";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 102;
            row[1] = "胡飞";
            row[2] = 2008;
            row[3] = true;
            row[4] = "信息工程";
            row[5] = 1;
            row[6] = 1;
            row[7] = DateTime.Now.AddDays(-90);
            row[8] = "09/01/2008";
            table.Rows.Add(row);

            row = table.NewRow();
            row[0] = 103;
            row[1] = "金婷婷";
            row[2] = 2001;
            row[3] = false;
            row[4] = "会计学";
            row[5] = 2;
            row[6] = 0;
            row[7] = DateTime.Now.AddDays(-80);
            row[8] = "09/01/2001";
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 104;
            row[1] = "潘国";
            row[2] = 2008;
            row[3] = false;
            row[4] = "国际经济与贸易";
            row[5] = 2;
            row[6] = 1;
            row[7] = DateTime.Now.AddDays(-70);
            row[8] = "09/01/2008";
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 105;
            row[1] = "吴颖颖";
            row[2] = 2002;
            row[3] = true;
            row[4] = "市场营销";
            row[5] = 3;
            row[6] = 0;
            row[7] = DateTime.Now.AddDays(-60);
            row[8] = "09/01/2002";
            table.Rows.Add(row);


            row = table.NewRow();
            row[0] = 106;
            row[1] = "张博";
            row[2] = 2003;
            row[3] = true;
            row[4] = "财务管理";
            row[5] = 3;
            row[6] = 1;
            row[7] = DateTime.Now.AddDays(-50);
            row[8] = "09/01/2003";
            table.Rows.Add(row);


            return table;
        }


    }
}