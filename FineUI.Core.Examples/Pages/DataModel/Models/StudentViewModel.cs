using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class StudentViewModel : Student
    {

        [Display(Name = "爱好")]
        public string HobbyInfo
        {
            get
            {
                List<string> hobbyNames = new List<string>();
                foreach (var hobby in Hobby)
                {
                    var hobbyName = "";
                    switch (hobby)
                    {
                        case "reading":
                            hobbyName = "读书";
                            break;
                        case "basketball":
                            hobbyName = "篮球";
                            break;
                        case "travel":
                            hobbyName = "旅游";
                            break;
                        case "movie":
                            hobbyName = "电影";
                            break;
                        case "music":
                            hobbyName = "音乐";
                            break;
                    }
                    hobbyNames.Add(hobbyName);
                }
                return String.Join(", ", hobbyNames.ToArray());
            }
        }


        [Display(Name = "家庭信息")]
        public string FamilyInfo
        {
            get
            {
                return "父亲: " + Family.FatherName + ", 母亲: " + Family.MotherName;
            }
        }


    }
}