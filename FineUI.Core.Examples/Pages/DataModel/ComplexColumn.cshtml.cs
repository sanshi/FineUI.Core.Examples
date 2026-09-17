using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using FineUI.Core.Examples.Pages.DataModel.Models;

namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class ComplexColumnModel : BaseModel
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
            Grid1.DataSource = StudentHelper.GetSimpleStudentList<StudentViewModel>();
            Grid1.DataBind();
        }


        public IList<StudentViewModel> Students { get; set; }

    }
}