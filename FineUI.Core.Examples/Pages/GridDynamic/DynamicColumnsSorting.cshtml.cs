using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class DynamicColumnsSortingModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (DropDownList1.SelectedValue == "table1")
            {
                Grid1.Title = "表格一（班级）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateClassGridColumns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    //AllowSorting = false,
                    //AllowPaging = false,
                    EnableCheckBoxSelect = false,
                }, DataSourceUtil.GetClassDataTable());
            }
            else
            {
                Grid1.Title = "表格二（学生，排序）";

                var recordCount = DataSourceUtil.GetTotalCount();

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGridColumns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    //AllowSorting = true,
                    //SortField = "Name",
                    //SortDirection = "ASC",
                    //AllowPaging = true,
                    //IsDatabasePaging = true,
                    //PageSize = 5,
                    //RecordCount = recordCount,
                    EnableCheckBoxSelect = true,
                }, DataSourceUtil.GetPagedDataTable(pageIndex: 0, pageSize: 5, sortField: "Name", sortDirection: "ASC"));
            }
        }

        #region 学生表格排序和分页

        /// <summary>
        /// 学生表格排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            LoadStudentData();
        }

        /// <summary>
        /// 学生表格分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChanged(object sender, GridPageEventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            var recordCount = DataSourceUtil.GetTotalCount();

            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = recordCount;

            // 2.获取当前分页数据
            Grid1.DataSource = DataSourceUtil.GetPagedDataTable(pageIndex: Grid1.PageIndex,
                pageSize: Grid1.PageSize,
                sortField: Grid1.SortField,
                sortDirection: Grid1.SortDirection);
            Grid1.DataBind();
        }   

        #endregion

        #region Events


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion

        #region CreateGridColumns

        private List<GridColumn> CreateClassGridColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            columns.Add(new RowNumberField());

            field = new RenderField();
            field.HeaderText = "班级名";
            field.DataField = "Name";
            field.SortField = "Name";
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "入学年份";
            field.DataField = "EntranceYear";
            field.SortField = "EntranceYear";
            field.FieldType = FieldType.Int;
            field.Width = 100;
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
            field.HeaderText = "描述";
            field.DataField = "Desc";
            field.ExpandUnusedSpace = true;
            columns.Add(field);

            return columns;
        }

        private List<GridColumn> CreateGridColumns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            columns.Add(new RowNumberField());

            field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "Name";
            field.SortField = "Name";
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "性别";
            field.DataField = "Gender";
            field.SortField = "Gender";
            field.FieldType = FieldType.Int;
            field.RendererFunction = "renderGender";
            field.Width = 80;
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "入学年份";
            field.DataField = "EntranceYear";
            field.SortField = "EntranceYear";
            field.FieldType = FieldType.Int;
            field.Width = 100;
            columns.Add(field);

            RenderCheckField checkfield = new RenderCheckField();
            checkfield.HeaderText = "是否在校";
            checkfield.DataField = "AtSchool";
            checkfield.RenderAsStaticField = true;
            checkfield.Width = 100;
            columns.Add(checkfield);

            checkfield = new RenderCheckField();
            checkfield.HeaderText = "是否在校";
            checkfield.DataField = "AtSchool";
            checkfield.RenderAsStaticField = false;
            checkfield.Enabled = false;
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

        #endregion


    }
}