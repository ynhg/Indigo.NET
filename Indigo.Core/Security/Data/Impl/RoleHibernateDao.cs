using Indigo.Infrastructure.Search;
using Indigo.Security.Search;
using NHibernate.Criterion;
using Spring.Stereotype;

namespace Indigo.Security.Data.Impl
{
    [Repository]
    public class RoleHibernateDao : GenericSecurityHibernateDao<Role, string>, IRoleDao
    {
        public Role GetAdminRole()
        {
            var query = CreateQuery("from Role where IsAdmin = true");
            return query.UniqueResult<Role>();
        }

        public Role GetByName(string name)
        {
            var query = CreateQuery("from Role where Name = :name").SetString("name", name);
            return query.UniqueResult<Role>();
        }

        public Page<Role> Search(RoleSearchForm searchForm)
        {
            var query = QueryOver();

            if (!string.IsNullOrWhiteSpace(searchForm.Name))
            {
                query.AndRestrictionOn(r => r.Name).IsLike(searchForm.Name, MatchMode.Anywhere);
            }

            query = query.OrderBy(e => e.Created).Desc;

            return GetPage(query, searchForm);
        }
    }
}
