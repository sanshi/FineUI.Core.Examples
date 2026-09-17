using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FineUI.Core.Examples.Pages.Toolbar
{
    public partial class FormFieldsModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        

        protected void btnClearDate_Click(object sender, EventArgs e)
        {
            dpStartDate.Reset();
            dpEndDate.Reset();

            Grid1.DataSource = null;
            Grid1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Grid1.DataSource = DataSourceUtil.GetDataTable();
            Grid1.DataBind();
        }
        
       
    }
}