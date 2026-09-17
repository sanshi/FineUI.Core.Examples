using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.MultiLang.Models
{
    public class User
    {
        [Display(Name = "Display_UserName")]
        [Required(ErrorMessage = "ErrorMessage_Required")]
        [StringLength(20, ErrorMessage = "ErrorMessage_StringLength")]
        public string UserName { get; set; }
        

        [Display(Name = "Display_Password")]
        [Required(ErrorMessage = "ErrorMessage_Required", AllowEmptyStrings = true)]
        [MaxLength(9, ErrorMessage = "ErrorMessage_MaxLength")]
        [MinLength(3, ErrorMessage = "ErrorMessage_MinLength")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?:[0-9]+[a-zA-Z]|[a-zA-Z]+[0-9])[a-zA-Z0-9]*$", ErrorMessage = "ErrorMessage_LetterAndNumber")]
        public string Password { get; set; }

    }
}