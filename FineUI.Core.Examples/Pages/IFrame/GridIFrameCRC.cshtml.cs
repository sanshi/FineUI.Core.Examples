using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.IFrame
{
    public partial class GridIFrameCRCModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoBindGrid();
            }

            Panel7.Title = "表格 - 页面加载时间：" + DateTime.Now.ToLongTimeString();
        }

        #region BindGrid

        private void AutoBindGrid()
        {
            var sourceKey = Grid1.Attributes.Value<string>("data-source-key");
            if (String.IsNullOrEmpty(sourceKey) || sourceKey == "table2")
            {
                BindGrid();
                sourceKey = "table1";
            }
            else
            {
                BindGrid2();
                sourceKey = "table2";
            }

            Grid1.Attributes["data-source-key"] = sourceKey;
        }

        private void BindGrid()
        {
            DataTable table = DataSourceUtil.GetDataTable();

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        private void BindGrid2()
        {
            DataTable table = DataSourceUtil.GetDataTable2();

            Grid1.DataSource = table;
            Grid1.DataBind();
        }

        #endregion

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            ShowNotify(e.SortField);
        }


        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            AutoBindGrid();
        }


        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "Grid1_DeleteRows")
            {
                // 选中行的标识由客户端随事件送上来，不用再去解回发的表格状态。
                // 事件参数完全由客户端提供，结构不能想当然，取不到就直接返回
                JArray rowIDs = e.EventArgumentsAsJObject["rowIDs"] as JArray;
                if (rowIDs == null)
                {
                    return;
                }

                // 这里只演示流程，不真删数据；实际项目在这里按主键删库，再重新绑定表格。
                // 注意只回显条数：行标识是客户端给的值，原样拼进消息文案不安全
                ShowNotify(String.Format("已删除 {0} 项数据！（仅演示，未真的删除）", rowIDs.Count));
            }
        }

        protected void ttbSearch_Trigger1Click(object sender, EventArgs e)
        {
            AutoBindGrid();

            ttbSearch.Text = String.Empty;
            ttbSearch.ShowTrigger1 = false;

        }

        protected void ttbSearch_Trigger2Click(object sender, EventArgs e)
        {
            AutoBindGrid();

            ttbSearch.ShowTrigger1 = true;
        }
    }
}