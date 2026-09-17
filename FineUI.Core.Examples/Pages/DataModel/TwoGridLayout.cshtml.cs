using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using FineUI.Core.Examples.Pages.DataModel.Models;


namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class TwoGridLayoutModel : BaseModel
    {
        public void OnGet()
        {
            var students = StudentHelper.GetSimpleStudentList();

            Students1 = students.Where(m => m.AtSchool).ToList();
            Students2 = students.Where(m => !m.AtSchool).ToList();
        }


        public IList<Student> Students1 { get; set; }

        public IList<Student> Students2 { get; set; }
        

    }
}