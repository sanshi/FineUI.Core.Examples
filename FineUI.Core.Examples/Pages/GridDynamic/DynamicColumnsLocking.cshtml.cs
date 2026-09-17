using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class DynamicColumnsLockingModel : BaseModel
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
                Grid1.Title = "表格一（单选）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid1Columns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    EnableCheckBoxSelect = false,
                    EnableMultiSelect = false,
                    AllowColumnLocking = false,
                }, DataSourceUtil.GetDataTable());
            }
            else
            {
                Grid1.Title = "表格二（多选，列锁定）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid2Columns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    EnableCheckBoxSelect = true,
                    EnableMultiSelect = true,
                    AllowColumnLocking = true,
                }, DataSourceUtil.GetDataTable2());
            }
        }


        private List<GridColumn> CreateGrid1Columns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            RowNumberField numberField = new RowNumberField();
            columns.Add(numberField);

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

            return columns;
        }

        private List<GridColumn> CreateGrid2Columns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "Name";
            field.EnableLock = true;
            field.Locked = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "性别";
            field.DataField = "Gender";
            field.EnableLock = true;
            field.Locked = false;
            field.FieldType = FieldType.Int;
            field.RendererFunction = "renderGender";
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "入学年份";
            field.DataField = "EntranceYear";
            field.EnableLock = true;
            field.FieldType = FieldType.Int;
            field.Width = 100;
            columns.Add(field);

            RenderCheckField checkfield = new RenderCheckField();
            checkfield.HeaderText = "是否在校";
            checkfield.DataField = "AtSchool";
            field.EnableLock = true;
            checkfield.RenderAsStaticField = true;
            columns.Add(checkfield);


            field = new RenderField();
            field.HeaderText = "语文成绩";
            field.DataField = "ChineseScore";
            field.EnableLock = true;
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "数学成绩";
            field.DataField = "MathScore";
            field.EnableLock = true;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "总成绩";
            field.DataField = "TotalScore";
            field.EnableLock = true;
            field.Locked = true;
            columns.Add(field);


            return columns;
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
