using Indigo.Infrastructure.Search;
using Indigo.Security.Search;

namespace Indigo.Security.Data
{
    public interface IRoleDao : IGenericSecurityDao<Role, string>
    {
        Role GetAdminRole();
        Role GetByName(string name);
        Page<Role> Search(RoleSearchForm searchForm);
    }
}
