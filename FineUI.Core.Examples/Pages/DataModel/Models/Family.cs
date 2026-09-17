using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class Family
    {
        [Display(Name = "父亲")]
        public string FatherName { get; set; }

        [Display(Name = "母亲")]
        public string MotherName { get; set; }

    }
}