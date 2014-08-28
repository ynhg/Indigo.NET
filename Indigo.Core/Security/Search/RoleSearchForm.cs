using Indigo.Infrastructure.Search;
using System.ComponentModel.DataAnnotations;

namespace Indigo.Security.Search
{
    public class RoleSearchForm : SearchForm
    {
        [Display(Name = "角色名")]
        public string Name { get; set; }
    }
}
