using System.ComponentModel.DataAnnotations;

namespace FineUI.Core.Examples.Pages.DataModel.Models
{
    /// <summary>
    ///学生状态
    /// </summary>
    public enum Status
    {
        [Display(Name = "优秀")]
        Excellent = 1,

        [Display(Name = "良好")]
        Good = 2,

        [Display(Name = "补考")]
        MakeUp = 3,

        [Display(Name = "重修")]
        Retake = 4
    }
}
