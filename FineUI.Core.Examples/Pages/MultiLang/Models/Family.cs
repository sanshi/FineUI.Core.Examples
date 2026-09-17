using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.MultiLang.Models
{
    public class Family
    {
        [Display(Name = "Display_Family_FatherName")]
        public string FatherName { get; set; }

        [Display(Name = "Display_Family_MotherName")]
        public string MotherName { get; set; }

    }
}