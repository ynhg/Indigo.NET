using Indigo.Infrastructure.Search;
using System.ComponentModel.DataAnnotations;

namespace Indigo.Security.Search
{
    public class UserSearchForm : SearchForm
    {
        [Display(Name = "用户名")]
        public string Name { get; set; }
    }
}
