using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class CheckBoxListModel
    {
        [Display(Name = "复选框一")]
        public bool IsChecked { get; set; }

        [Required]
        [Display(Name = "复选框列表一")]
        public string[] CheckBoxList1 { get; set; }


        public List<CheckItem> CheckBoxList1Items { get; set; }


        [Required]
        [Display(Name = "单选框列表一")]
        public string RadioButtonList1 { get; set; }

        public List<RadioItem> RadioButton1Items { get; set; }


        [Required]
        [Display(Name = "性别")]
        public int Gender { get; set; }


        
        [Required]
        [Display(Name = "性别（枚举值）")]
        public GenderType GenderType { get; set; }

    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        /// <summary>
        /// 女
        /// </summary>
        Woman = 0,

        /// <summary>
        /// 男
        /// </summary>
        Man = 1
    }

}