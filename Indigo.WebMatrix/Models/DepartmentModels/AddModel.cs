using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Models.DepartmentModels
{
    public class AddModel
    {
        [Required]
        [Remote("IsNameUnique", "Department", ErrorMessage = "{0}已经被使用")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "上级部门")]
        public string SuperDepartmentId { get; set; }

        [StringLength(200)]
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}