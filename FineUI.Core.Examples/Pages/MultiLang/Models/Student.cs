using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.MultiLang.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Display_Student_Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display_Student_Gender")]
        public int Gender { get; set; }

        [Required]
        [Display(Name = "Display_Student_EntranceYear")]
        public int EntranceYear { get; set; }

        [Required]
        [Display(Name = "Display_Student_AtSchool")]
        public bool AtSchool { get; set; }

        [Required]
        [Display(Name = "Display_Student_Major")]
        [StringLength(200)]
        public string Major { get; set; }

        [Required]
        [Display(Name = "Display_Student_Group")]
        public int Group { get; set; }


        [Display(Name = "Display_Student_EntranceDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? EntranceDate { get; set; }



        [Display(Name = "Display_Student_Hobby")]
        public string[] Hobby { get; set; }


        [Display(Name = "Display_Student_Family")]
        public Family Family { get; set; }


        [Display(Name = "Display_Student_Score")]
        public Score Score { get; set; }




    }
}