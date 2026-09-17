using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class RowCommandTextIconModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Grid1_RowCommand(object sender, GridRowCommandEventArgs e)
        {
            if (e.CommandName == "Action1" || e.CommandName == "Action2" || e.CommandName == "Action3")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                ShowNotify(String.Format("你点击了第 {0} 行，第 {1} 列，行命令：{2}，行ID：{3}，姓名：{4}",
                    e.RowIndex + 1,
                    e.ColumnIndex + 1,
                    e.CommandName,
                    keys[0], keys[1]));
            }
        }


    }
}