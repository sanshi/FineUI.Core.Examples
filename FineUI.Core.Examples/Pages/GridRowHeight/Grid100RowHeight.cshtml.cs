using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using Newtonsoft.Json.Linq;


namespace FineUI.Core.Examples.Pages.GridRowHeight
{
    public partial class Grid100RowHeightModel : BaseModel
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
            DataTable table = DataSourceUtil.GetDataTable();

            int newtableID = 101;
            DataTable newtable = table.Clone();
            for (int i = 0; i <= 8; i++)
            {
                foreach (DataRow row in table.Rows)
                {
                    newtable.ImportRow(row);

                    var newImportedRow = newtable.Rows[newtable.Rows.Count - 1];
                    newImportedRow["Id"] = newtableID;
                    if (i > 0)
                    {
                        newImportedRow["Name"] = String.Format("{0}（{1}）", newImportedRow["Name"], i);
                    }

                    newtableID++;
                }
            }

            Grid1.DataSource = newtable;
            Grid1.DataBind();
        }



    }
}