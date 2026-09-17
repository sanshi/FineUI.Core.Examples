using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class DynamicColumnsPostBackModel : BaseModel
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
            if (DropDownList1.SelectedValue == "table1")
            {
                Grid1.Title = "表格一（单选，行扩展列）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid1Columns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    EnableCheckBoxSelect = false,
                    EnableMultiSelect = false,
                }, DataSourceUtil.GetDataTable());
            }
            else
            {
                Grid1.Title = "表格二（多选，多表头，全选列）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid2Columns(), new GridConfigOptions()
                {
                    DataIDField = "Guid",
                    DataTextField = "Year",
                    EnableCheckBoxSelect = true,
                    EnableMultiSelect = true,
                }, GetDataTable2());
            }

        }


        private List<GridColumn> CreateGrid1Columns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "Name";
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "性别";
            field.DataField = "Gender";
            field.FieldType = FieldType.Int;
            field.RendererFunction = "renderGender";
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "入学年份";
            field.DataField = "EntranceYear";
            field.FieldType = FieldType.Int;
            field.Width = 100;
            columns.Add(field);

            RenderCheckField checkfield = new RenderCheckField();
            checkfield.HeaderText = "是否在校";
            checkfield.DataField = "AtSchool";
            checkfield.RenderAsStaticField = true;
            columns.Add(checkfield);

            checkfield = new RenderCheckField();
            checkfield.HeaderText = "是否在校";
            checkfield.DataField = "AtSchool";
            checkfield.RenderAsStaticField = false;
            checkfield.EnableColumnEdit = false;
            columns.Add(checkfield);


            field = new RenderField();
            field.HeaderText = "所学专业";
            field.DataField = "Major";
            field.RendererFunction = "renderMajor";
            field.ExpandUnusedSpace = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "分组";
            field.DataField = "Group";
            field.RendererFunction = "renderGroup";
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "注册日期";
            field.DataField = "LogTime";
            field.FieldType = FieldType.Date;
            field.Renderer = Renderer.Date;
            field.RendererArgument = "yyyy/MM/dd";
            field.Width = 100;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "个人简介";
            field.DataField = "Desc";
            field.RenderAsRowExpander = true;
            columns.Add(field);

            return columns;
        }


        private List<GridColumn> CreateGrid2Columns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;


            columns.Add(new RowNumberField());

            // 统计年份
            field = new RenderField();
            field.HeaderText = "统计年份";
            field.DataField = "Year";
            field.Width = 80;
            columns.Add(field);

            // 安徽省 - 合肥市
            GroupField groupFieldAnHui = new GroupField();
            groupFieldAnHui.HeaderText = "安徽省";
            groupFieldAnHui.TextAlign = TextAlign.Center;

            GroupField groupFieldHefei = new GroupField();
            groupFieldHefei.HeaderText = "合肥市";
            groupFieldHefei.TextAlign = TextAlign.Center;

            field = new RenderField();
            field.HeaderText = "数据一";
            field.DataField = "AHData1";
            field.Width = 100;
            groupFieldHefei.Columns.Add(field);

            field = new RenderField();
            field.HeaderText = "数据二";
            field.DataField = "AHData2";
            field.Width = 100;
            groupFieldHefei.Columns.Add(field);

            groupFieldAnHui.Columns.Add(groupFieldHefei);

            columns.Add(groupFieldAnHui);


            // 河南省 - 驻马店
            GroupField groupFieldHeNan = new GroupField();
            groupFieldHeNan.HeaderText = "河南省";
            groupFieldHeNan.TextAlign = TextAlign.Center;

            GroupField groupFieldZhuMaDian = new GroupField();
            groupFieldZhuMaDian.HeaderText = "驻马店市";
            groupFieldZhuMaDian.TextAlign = TextAlign.Center;

            field = new RenderField();
            field.HeaderText = "数据一";
            field.DataField = "HZData1";
            field.Width = 100;
            groupFieldZhuMaDian.Columns.Add(field);

            field = new RenderField();
            field.HeaderText = "数据二";
            field.DataField = "HZData2";
            field.Width = 100;
            groupFieldZhuMaDian.Columns.Add(field);

            groupFieldHeNan.Columns.Add(groupFieldZhuMaDian);

            // 河南省 - 漯河市
            GroupField groupFieldLuoHe = new GroupField();
            groupFieldLuoHe.HeaderText = "漯河市";
            groupFieldLuoHe.TextAlign = TextAlign.Center;

            field = new RenderField();
            field.HeaderText = "数据一";
            field.DataField = "HLData1";
            field.Width = 100;
            groupFieldLuoHe.Columns.Add(field);

            field = new RenderField();
            field.HeaderText = "数据二";
            field.DataField = "HLData2";
            field.Width = 100;
            groupFieldLuoHe.Columns.Add(field);

            groupFieldHeNan.Columns.Add(groupFieldLuoHe);

            columns.Add(groupFieldHeNan);

            // 记录时间
            field = new RenderField();
            field.HeaderText = "记录时间";
            field.DataField = "LogTime";
            field.TextAlign = TextAlign.Center;
            field.FieldType = FieldType.Date;
            field.Renderer = Renderer.Date;
            field.RendererArgument = "yyyy/MM/dd";

            // 回发时动态创建的列 - 默认不显示[记录时间]列
            field.Hidden = true;

            columns.Add(field);


            return columns;
        }

        private DataTable GetDataTable2()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Guid", typeof(Guid)));
            table.Columns.Add(new DataColumn("Year", typeof(int)));
            table.Columns.Add(new DataColumn("HZData1", typeof(int)));
            table.Columns.Add(new DataColumn("HZData2", typeof(int)));
            table.Columns.Add(new DataColumn("HLData1", typeof(int)));
            table.Columns.Add(new DataColumn("HLData2", typeof(int)));
            table.Columns.Add(new DataColumn("AHData1", typeof(int)));
            table.Columns.Add(new DataColumn("AHData2", typeof(int)));
            table.Columns.Add(new DataColumn("LogTime", typeof(DateTime)));


            DataRow row;

            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int year = 2000 + i;

                row = table.NewRow();
                row[0] = Guid.NewGuid();
                row[1] = year;
                row[2] = rd.Next(1000, 9999);
                row[3] = rd.Next(1000, 9999);
                row[4] = rd.Next(1000, 9999);
                row[5] = rd.Next(1000, 9999);
                row[6] = rd.Next(1000, 9999);
                row[7] = rd.Next(1000, 9999);
                row[8] = DateTime.Parse(String.Format("{0}-09-01", year));

                table.Rows.Add(row);
            }

            return table;
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}