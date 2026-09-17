using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridTree
{
    public partial class ExpandOnDblClickModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        
        protected void Grid1_RowDoubleClick(object sender, GridRowEventArgs e)
        {
            //ShowNotify(String.Format("你双击了第 {0} 行，行ID：{1}，名称：{2}",
            //    rowInfo.Value<int>("index"),
            //    rowInfo.Value<string>("id"),
            //    rowInfo.Value<string>("text")));

            object[] keys = Grid1.DataKeys[e.RowIndex];
            ShowNotify(String.Format("你双击了第 {0} 行，行ID：{1}，名称：{2}", e.RowIndex + 1, keys[0], keys[1]));
        }

    }
}