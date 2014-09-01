using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Indigo.Organization;

namespace Indigo.WebMatrix.Models.EmployeeModels
{
    public class AddModel
    {
        [Required, StringLength(20)]
        [Remote("IsNumberUnique", "Employee", ErrorMessage = "{0}已经被使用")]
        [Display(Name = "工号")]
        public string Number { get; set; }

        [Required, StringLength(10)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "身份证号")]
        public string IdentityCardNumber { get; set; }

        [Display(Name = "性别")]
        public Gender Gender { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "年龄")]
        public int Age { get; set; }

        [Display(Name = "部门")]
        public string DepartmentId { get; set; }

        [Display(Name = "职位")]
        public string PositionId { get; set; }
    }
}