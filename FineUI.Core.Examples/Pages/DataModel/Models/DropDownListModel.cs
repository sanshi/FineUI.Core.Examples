using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class DropDownListModel
    {
        
        [Required]
        [Display(Name = "下拉列表一")]
        public string DropDownList1 { get; set; }


        public List<ListItem> DropDownList1Items { get; set; }


        [Required]
        [Display(Name = "下拉列表二")]
        public string[] DropDownList2 { get; set; }

        public List<ListItem> DropDownList2Items { get; set; }

    }
}