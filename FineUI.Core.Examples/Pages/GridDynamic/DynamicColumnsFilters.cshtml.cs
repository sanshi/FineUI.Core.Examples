using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace FineUI.Core.Examples.Pages.GridDynamic
{
    public partial class DynamicColumnsFiltersModel : BaseModel
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
                Grid1.Title = "表格一";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid1Columns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    EnableCheckBoxSelect = false,
                    EnableMultiSelect = false,
                    //AllowFilters = false,
                }, DataSourceUtil.GetDataTable());
            }
            else
            {
                Grid1.Title = "表格二（表头过滤）";

                // 重新配置表头并绑定数据
                Grid1.ConfigColumns(CreateGrid2Columns(), new GridConfigOptions()
                {
                    DataIDField = "Id",
                    DataTextField = "Name",
                    EnableCheckBoxSelect = true,
                    EnableMultiSelect = true,
                    //AllowFilters = true,
                    //InlineFilters = true,
                }, DataSourceUtil.GetDataTable2());
            }
        }

        #region CreateGrid1Columns

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

        #endregion

        #region CreateGrid2Columns

        private List<GridColumn> CreateGrid2Columns()
        {
            List<GridColumn> columns = new List<GridColumn>();

            RenderField field = null;

            field = new RenderField();
            field.HeaderText = "姓名";
            field.DataField = "Name";
            field.Width = 200;
            field.EnableFilter = true;

            field.Filter = new FineUI.Core.GridFilter();
            var ddlOperator = new FineUI.Core.DropDownList
            {
                Items =
                {
                    new ListItem("等于", "equal"),
                    new ListItem("包含", "contain", true),
                    new ListItem("开始于", "start"),
                    new ListItem("结束于", "end")
                }
            };
            field.Filter.Operator.Add(ddlOperator);

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
            field.HeaderText = "语文成绩";
            field.DataField = "ChineseScore";
            columns.Add(field);


            field = new RenderField();
            field.HeaderText = "数学成绩";
            field.DataField = "MathScore";
            columns.Add(field);

            field = new RenderField();
            field.HeaderText = "总成绩";
            field.DataField = "TotalScore";
            field.Locked = true;
            columns.Add(field);


            return columns;
        }
        #endregion

        #region Grid1_FilterChanged

        protected void Grid1_FilterChanged(object sender, EventArgs e)
        {
            NewFilteredTable filteredTable = new NewFilteredTable();
            filteredTable.FilterDataRowItem = FilterDataRowItemImplement;

            DataTable table = filteredTable.GetFilteredTable(Grid1);
            Grid1.DataSource = table;
            Grid1.DataBind();

            labResult.Text = String.Format("过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }


        #region FilterDataRowItem

        private bool FilterDataRowItemImplement(object sourceObj, GridColumnFilteredItem filteredItem, string columnID)
        {
            bool valid = false;

            string fillteredOperator = filteredItem.Operator;
            if (columnID == "Name")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = filteredItem.Value.ToString();
                if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "contain")
                {
                    if (sourceValue.Contains(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "start")
                {
                    if (sourceValue.StartsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "end")
                {
                    if (sourceValue.EndsWith(fillteredValue))
                    {
                        valid = true;
                    }
                }
            }

            return valid;
        }

        #endregion

        #endregion

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
