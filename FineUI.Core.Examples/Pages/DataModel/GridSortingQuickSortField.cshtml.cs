using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FineUI.Core.Examples.Pages.DataModel.Models;

namespace FineUI.Core.Examples.Pages.DataModel
{
    
    public partial class GridSortingQuickSortFieldModel : BaseModel
    {
        public IList<Student> Students { get; set; }

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

        #region LoadData

        private void LoadData()
        {
            Grid1.DataSource = GetSortedDataTable(Grid1.SortField, Grid1.SortDirection);
            Grid1.DataBind();
        }

        private IList<Student> GetSortedDataTable(string sortField, string sortDirection)
        {
            var students = StudentHelper.GetSimpleStudentList().ToList();
            students.Sort((left, right) =>
            {
                if (sortField == "Name")
                {
                    return sortDirection == "ASC" ? left.Name.CompareTo(right.Name) : right.Name.CompareTo(left.Name);
                }
                else if (sortField == "Gender")
                {
                    return sortDirection == "ASC" ? left.Gender.CompareTo(right.Gender) : right.Gender.CompareTo(left.Gender);
                }
                else if (sortField == "EntranceYear")
                {
                    return sortDirection == "ASC" ? left.EntranceYear.CompareTo(right.EntranceYear) : right.EntranceYear.CompareTo(left.EntranceYear);
                }
                else if (sortField == "AtSchool")
                {
                    return sortDirection == "ASC" ? left.AtSchool.CompareTo(right.AtSchool) : right.AtSchool.CompareTo(left.AtSchool);
                }
                return 0;
            });

            return students;
        }

        #endregion

        public void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            LoadData();
        }

    }
}