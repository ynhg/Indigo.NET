using System.ComponentModel.DataAnnotations;
using Indigo.Infrastructure.Search;

namespace Indigo.Security.Search
{
    public class RoleSearchForm : SearchForm
    {
        [Display(Name = "角色名")]
        public string Name { get; set; }
    }
}