using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class Score
    {
        [Display(Name = "语文")]
        public int Chinese { get; set; }

        [Display(Name = "数学")]
        public int Math { get; set; }

        [Display(Name = "物理")]
        public int Physics { get; set; }

        [Display(Name = "化学")]
        public int Chemistry { get; set; }

    }
}