using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Indigo.WebMatrix.Models.PositionModels
{
    public class AddModel
    {
        [Required]
        [Remote("IsNameUnique", "Position", ErrorMessage = "{0}已经被使用")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "级别")]
        public int Rank { get; set; }
    }
}