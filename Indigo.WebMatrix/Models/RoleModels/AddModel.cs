using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Models.RoleModels
{
    public class AddModel
    {
        [Required]
        [Remote("IsNameUnique", ErrorMessage = "{0}已经被使用")]
        [Display(Name = "角色名")]
        public string RoleName { get; set; }

        [StringLength(200)]
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}