using Indigo.Infrastructure.Search;
using Indigo.Security.Search;

namespace Indigo.Security.Data
{
    public interface IUserDao : IGenericSecurityDao<User, string>
    {
        Page<User> Search(UserSearchForm searchForm);
        User GetByName(string name);
    }
}