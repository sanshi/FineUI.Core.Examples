using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.GridPaging
{
    public partial class PagingTypeModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var pagingType = Request.Query["type"];
                if (String.IsNullOrEmpty(pagingType))
                {
                    // 缺省选中值
                    pagingType = "ArrowNumberBox";
                }
                Grid1.PagingType = (PagingType)Enum.Parse(typeof(PagingType), pagingType, true);

                if (pagingType == "NumberButton" || pagingType == "ArrowNumberButton")
                {
                    ddlMaxPagingNumberButton.Enabled = true;
                }
                else
                {
                    ddlMaxPagingNumberButton.Enabled = false;
                }


                var isShowMessage = true;
                var showMessage = Request.Query["message"];
                if (!String.IsNullOrEmpty(showMessage))
                {
                    isShowMessage = Convert.ToBoolean(showMessage);
                }
                Grid1.ShowPagingMessage = isShowMessage;
                cbxShowPagingMessage.Checked = isShowMessage;


                var maxNumberButtonCount = 5;
                var maxNumberButton = Request.Query["maxnumberbutton"];
                if (!String.IsNullOrEmpty(maxNumberButton))
                {
                    maxNumberButtonCount = Convert.ToInt32(maxNumberButton);
                }
                Grid1.MaxPagingNumberButton = maxNumberButtonCount;


                // 初始化选中的分页工具栏类型
                foreach (var btn in bgPagingType.Items)
                {
                    if (btn.Text == pagingType)
                    {
                        btn.Pressed = true;
                        break;
                    }
                }

                LoadData();
            }
        }


        
        #region BindGrid

        private void LoadData()
        {
            // 1.设置总项数（特别注意：数据库分页初始化时，一定要设置总记录数RecordCount）
            Grid1.RecordCount = GetTotalCount();

            // 2.获取当前分页数据
            Grid1.DataSource = GetPagedDataTable(pageIndex: Grid1.PageIndex, pageSize: Grid1.PageSize);
            Grid1.DataBind();
        }

        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        private int GetTotalCount()
        {
            return 999;
        }

        /// <summary>
        /// 模拟数据库分页（实际项目中请直接使用SQL语句返回分页数据！）
        /// </summary>
        /// <returns></returns>
        private DataTable GetPagedDataTable(int pageIndex, int pageSize)
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("EntranceTime", typeof(DateTime)));

            DataRow row = null;

            for (int i = pageIndex * pageSize, count = Math.Min(GetTotalCount(), pageIndex * pageSize + pageSize); i < count; i++)
            {
                row = table.NewRow();
                row[0] = 1000 + i;
                row[1] = DateTime.Now.AddSeconds(i);
                table.Rows.Add(row);
            }

            return table;
        }

        #endregion

        protected void Grid1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}