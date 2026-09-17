using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.MultiLang.Models
{
    public class UICompareModel
    {

        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "ErrorMessage_ConfirmPassword")]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_StartDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_EndDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [UICompare("StartDate", Operator.GreaterThan, ErrorMessage = "ErrorMessage_UICompare_GreaterThan")]
        public DateTime? EndDate { get; set; }


        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_Number1")]
        public int? Number1 { get; set; }

        [Required(ErrorMessage = "ErrorMessage_Required")]
        [Display(Name = "Display_Number2")]
        [UICompare("Number1", Operator.GreaterThanEqual, ErrorMessage = "ErrorMessage_UICompare_GreaterThanEqual")]
        public int? Number2 { get; set; }


    }
}