using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "姓名")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "性别")]
        public int Gender { get; set; }

        [Required]
        [Display(Name = "入学年份")]
        public int EntranceYear { get; set; }

        [Required]
        [Display(Name = "是否在校")]
        public bool AtSchool { get; set; }

        [Required]
        [Display(Name = "所学专业")]
        [StringLength(200)]
        public string Major { get; set; }

        [Required]
        [Display(Name = "分组")]
        public int Group { get; set; }


        [Display(Name = "注册日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? EntranceDate { get; set; }



        [Display(Name = "爱好")]
        public string[] Hobby { get; set; }


        [Display(Name = "家庭信息")]
        public Family Family { get; set; }


        [Display(Name = "考试成绩")]
        public Score Score { get; set; }


        [Display(Name = "状态")]
        public Status CurrentStatus { get; set; }



        [Display(Name = "状态（Display）")]
        public string CurrentStatusDisplay
        {
            get
            {
                return GetDisplayName<Status>(CurrentStatus);
            }
        }


        // 获取枚举类型的Display注解（支持任意枚举类型和可空枚举类型）
        private static string GetDisplayName<T>(T? status) where T : struct, Enum
        {
            if (!status.HasValue)
            {
                return ""; // 枚举值为空时，返回空字符串
            }

            var enumValue = status.Value;
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            var displayAttribute = memberInfo?
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.GetName() ?? enumValue.ToString();
        }

    }
}