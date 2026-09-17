using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class StudentExtended : Student
    {
        [Display(Name = "爱好")]
        public JArray HobbyArray
        {
            get
            {
                return new JArray(Hobby);
            }
        }


        [Display(Name = "家庭信息")]
        public JObject FamilyObject
        {
            get
            {
                JObject jo = new JObject();
                jo.Add("daddy", Family.FatherName);
                jo.Add("mommy", Family.MotherName);
                return jo;
            }
        }


    }
}