using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FineUI.Core.Examples.Pages.DataModel.Models;


namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class ComplexPropertyModel : BaseModel
    {
        public void OnGet()
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            Grid1.DataSource = StudentHelper.GetSimpleStudentList<Student>();
            Grid1.DataBind();
        }


        public IList<Student> Students { get; set; }
        

    }
}