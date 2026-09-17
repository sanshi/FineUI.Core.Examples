using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static FineUI.Core.Examples.Pages.GridOther.ComplexPropertyModel;

namespace FineUI.Core.Examples.Pages.GridOther
{
    public partial class LongPrecisionModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Grid1.DataSource = GetSimpleDataTable();
                Grid1.DataBind();


                //var dataTable = GetSimpleDataTable();
                //// 表达式转换
                //dataTable.Columns.Add("IdString", typeof(string), "Convert(Id, 'System.String')");

                //var dataTable = GetSimpleDataTable();
                //dataTable.Columns.Add("IdString", typeof(string));

                //foreach(DataRow row in dataTable.Rows)
                //{
                //    row["IdString"] = row["Id"].ToString();
                //}

                //Grid1.DataSource = dataTable;
                //Grid1.DataBind();

                //var dataList = GetSimpleDataList();
                //List<ExtendedStudent> wrappedList = dataList.Select(x => new ExtendedStudent
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    EntranceYear = x.EntranceYear,
                //    AtSchool = x.AtSchool,
                //    Major = x.Major,
                //    Gender = x.Gender,
                //    EntranceDate = x.EntranceDate
                //}).ToList();

                //Grid1.DataSource = wrappedList;
                //Grid1.DataBind();
            }
        }

        #region GetSimpleDataTable

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



        #endregion

        #region Student

        // 直接继承并添加新属性
        public class ExtendedStudent : Student
        {
            public string IdString => Id.ToString();
        }

        public class Student
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public int EntranceYear { get; set; }
            public bool AtSchool { get; set; }
            public string Major { get; set; }
            public int Gender { get; set; }
            public string EntranceDate { get; set; }
        }

        public List<Student> GetSimpleDataList()
        {
            return GetSimpleDataTable().AsEnumerable().Select(row => new Student
            {
                Id = row.Field<long>("Id"),
                Name = row.Field<string>("Name"),
                EntranceYear = row.Field<int>("EntranceYear"),
                AtSchool = row.Field<bool>("AtSchool"),
                Major = row.Field<string>("Major"),
                Gender = row.Field<int>("Gender"),
                EntranceDate = row.Field<string>("EntranceDate")
            }).ToList();
        }

        #endregion

    }
}