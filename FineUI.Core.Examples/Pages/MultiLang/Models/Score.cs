using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.MultiLang.Models
{
    public class Score
    {
        [Display(Name = "Display_Score_Chinese")]
        public int Chinese { get; set; }

        [Display(Name = "Display_Score_Math")]
        public int Math { get; set; }

        [Display(Name = "Display_Score_Physics")]
        public int Physics { get; set; }

        [Display(Name = "Display_Score_Chemistry")]
        public int Chemistry { get; set; }

    }
}