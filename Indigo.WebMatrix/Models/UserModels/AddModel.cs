using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Models.UserModels
{
    public class AddModel
    {
        [Required]
        [Remote("IsNameUnique", ErrorMessage = "{0}已经被使用")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "{0}必须至少包含{2}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }
    }
}