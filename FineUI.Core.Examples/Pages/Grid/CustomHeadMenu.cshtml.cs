using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CustomHeadMenuModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGrid();
            }
        }



        private void BindGrid()
        {
            DataTable table = DataSourceUtil.GetDataTable();

            DataView view = table.DefaultView;

            List<string> filters = new List<string>();
            if (btnSelectAtSchool.Checked)
            {
                filters.Add("AtSchool=1");
            }
            else if (btnSelectNotAtSchool.Checked)
            {
                filters.Add("AtSchool=0");
            }

            if (btnEntranceYearGreatThan2002.Checked)
            {
                filters.Add("EntranceYear>2002");
            }

            if (filters.Count > 0)
            {
                view.RowFilter = String.Join(" AND ", filters.ToArray());
            }

            Grid1.DataSource = view.ToTable();
            Grid1.DataBind();
        }


        protected void btnAtSchool_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }


        protected void btnEntranceYear_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

    }
}