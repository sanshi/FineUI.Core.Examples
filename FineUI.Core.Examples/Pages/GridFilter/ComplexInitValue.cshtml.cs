using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridFilter
{
    public partial class ComplexInitValueModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化列的过滤数据
                var columnEntranceYear = Grid1.FindColumn("EntranceYear");
                columnEntranceYear.ColumnFilteredData = new GridColumnFilteredData()
                {
                    Multi = true,
                    Matcher = "all",
                    Items = {
                        new GridColumnFilteredItem() {
                            Operator = "greater",
                            Value = 2008
                        },
                        new GridColumnFilteredItem() {
                            Operator = "less",
                            Value = 2025
                        }
                    }
                };

                var columnMajor = Grid1.FindColumn("Major");
                columnMajor.ColumnFilteredData = new GridColumnFilteredData()
                {
                    Multi = false,
                    Items = {
                        new GridColumnFilteredItem() {
                            Value = new string[] { "化学系", "物理系", "数学系" }
                        }
                    }
                };

                var columnGroup = Grid1.FindColumn("Group");
                columnGroup.ColumnFilteredData = new GridColumnFilteredData()
                {
                    Multi = false,
                    Items = {
                        new GridColumnFilteredItem() {
                            Value = new string[] { "2", "3" }   // 分组2, 分组3
                        }
                    }
                };


                BindGrid();

                labResult.Text = String.Format("初始过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));

                InitFilterGroupList();
            }
        }



        private void InitFilterGroupList()
        {
            groupList.Items.Clear();
            for (int i = 1; i <= 5; i++)
            {
                ListItem item = new ListItem();
                item.Value = i.ToString();
                item.Text = String.Format("分组{0}", i);
                item.Display = String.Format("<img src=\"{0}\">&nbsp;{1}", Url.Content("~/res/images/16/" + i.ToString() + ".png"), item.Text);

                groupList.Items.Add(item);
            }

        }

        protected void Grid1_FilterChanged(object sender, EventArgs e)
        {
            BindGrid();

            labResult.Text = String.Format("过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }

        private void BindGrid()
        {
            NewFilteredTable filteredTable = new NewFilteredTable();
            filteredTable.FilterDataRowItem = FilterDataRowItemImplement;

            DataTable table = filteredTable.GetFilteredTable(Grid1);

            Grid1.DataSource = table;
            Grid1.DataBind();
        }



        protected void btnUpdateFilteredData_Click(object sender, EventArgs e)
        {
            // 先清空过滤数据
            Grid1.FilteredData = null;

            // 初始化列的过滤数据
            var columnGender = Grid1.FindColumn("Gender");
            columnGender.ColumnFilteredData = new GridColumnFilteredData()
            {
                Multi = false,
                Items = {
                    new GridColumnFilteredItem() {
                        Value = 1
                    }
                }
            };

            var columnEntranceYear = Grid1.FindColumn("EntranceYear");
            columnEntranceYear.ColumnFilteredData = new GridColumnFilteredData()
            {
                Multi = true,
                Matcher = "all",
                Items = {
                        new GridColumnFilteredItem() {
                            Operator = "greater",
                            Value = 2008
                        },
                        new GridColumnFilteredItem() {
                            Operator = "less",
                            Value = 2025
                        }
                    }
            };


            BindGrid();

            labResult.Text = String.Format("后台更新过滤数据：<pre>{0}</pre>", EncodeJson(Grid1.FilteredData));
        }


        #region FilterDataRowItem

        private bool FilterDataRowItemImplement(object sourceObj, GridColumnFilteredItem filteredItem, string columnID)
        {
            bool valid = false;

            string fillteredOperator = filteredItem.Operator;
            if (columnID == "Name")
            {
                string sourceValue = sourceObj.ToString();
                string fillteredValue = filteredItem.Value.ToString();  //fillteredObj.ToString();
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
            else if (columnID == "EntranceYear")
            {
                int sourceValue = Convert.ToInt32(sourceObj);
                int fillteredValue = Convert.ToInt32(filteredItem.Value);

                if (fillteredOperator == "greater")
                {
                    if (sourceValue > fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "less")
                {
                    if (sourceValue < fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }

            }
            else if (columnID == "LogTime")
            {
                // 时间比较时要去掉数据源中的时分秒！
                DateTime sourceDate = Convert.ToDateTime(sourceObj);
                DateTime sourceValue = new DateTime(sourceDate.Year, sourceDate.Month, sourceDate.Day);

                DateTime fillteredValue = Convert.ToDateTime(filteredItem.Value);

                if (fillteredOperator == "greater")
                {
                    if (sourceValue > fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "less")
                {
                    if (sourceValue < fillteredValue)
                    {
                        valid = true;
                    }
                }
                else if (fillteredOperator == "equal")
                {
                    if (sourceValue == fillteredValue)
                    {
                        valid = true;
                    }
                }

            }
            else if (columnID == "Major" || columnID == "Group")
            {
                string sourceValue = sourceObj.ToString();
                string[] fillteredValue = (string[])filteredItem.Value;

                foreach (string filltereditem in fillteredValue)
                {
                    if (filltereditem == sourceValue)
                    {
                        valid = true;
                        break;
                    }
                }
            }
            else if (columnID == "AtSchool")
            {
                bool sourceValue = Convert.ToBoolean(sourceObj);
                bool fillteredValue = Convert.ToBoolean(filteredItem.Value);

                if (sourceValue == fillteredValue)
                {
                    valid = true;
                }
            }
            else if (columnID == "Gender")
            {
                int sourceValue = Convert.ToInt32(sourceObj);
                int fillteredValue = Convert.ToInt32(filteredItem.Value);

                if (sourceValue == fillteredValue)
                {
                    valid = true;
                }
            }


            return valid;
        }

        #endregion


    }
}