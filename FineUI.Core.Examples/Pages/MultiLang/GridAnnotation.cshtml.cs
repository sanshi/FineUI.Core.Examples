using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using FineUI.Core.Examples.Pages.MultiLang.Models;
using Newtonsoft.Json.Linq;

namespace FineUI.Core.Examples.Pages.MultiLang
{
    
    public partial class GridAnnotationModel : BaseMultilangModel
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
            Grid1.DataSource = StudentHelper.GetSimpleStudentList();
            Grid1.DataBind();
        }


        public IList<Student> Students { get; set; }



    }
}