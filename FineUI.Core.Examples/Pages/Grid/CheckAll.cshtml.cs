using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Xml.Linq;

namespace FineUI.Core.Examples.Pages.Grid
{
    public partial class CheckAllModel : BaseModel
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
            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();

            Grid1.SelectedRowIDArray = new string[] { "105", "110" };
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Grid1.SelectedRowIDArray = new string[] { "102", "106", "108" };
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowNotify(new RawHtml(GridSelectionMessage.HowManyRowsAreSelected(Grid1)));
        }


    }
}
