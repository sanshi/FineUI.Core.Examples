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
    public partial class GridIFrameReloadModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AutoBindGrid();
            }
        }

        protected void Page_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventName == "CloseWindow1")
            {
                AutoBindGrid();
                Grid1.Title = "表格 - 回发参数：" + e.EventArguments;
            }
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


        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            AutoBindGrid();

            Grid1.Title = "表格 - 回发参数：" + (!String.IsNullOrEmpty(e.CloseArgument) ? e.CloseArgument : "Window1_Close");
        }



    }
}