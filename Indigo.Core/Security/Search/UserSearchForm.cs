using System.ComponentModel.DataAnnotations;
using Indigo.Infrastructure.Search;

namespace Indigo.Security.Search
{
    public class UserSearchForm : SearchForm
    {
        [Display(Name = "用户名")]
        public string Name { get; set; }
    }
}