using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class User
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}不能为空！")]
        [StringLength(20)]
        public string UserName { get; set; }
        

        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}不能为空！", AllowEmptyStrings = true)]
        [MaxLength(9, ErrorMessage = "{0}最大为 {1} 个字符！")]
        [MinLength(3, ErrorMessage = "{0}最小为 {1} 个字符！")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?:[0-9]+[a-zA-Z]|[a-zA-Z]+[0-9])[a-zA-Z0-9]*$", ErrorMessage = "{0}至少包含一个字母和数字！")]
        public string Password { get; set; }

    }
}