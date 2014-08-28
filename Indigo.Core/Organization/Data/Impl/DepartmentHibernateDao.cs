using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data.Impl;
using Spring.Stereotype;

namespace Indigo.Organization.Data.Impl
{
    [Repository]
    public class DepartmentHibernateDao : GenericSecurityHibernateDao<Department, string>, IDepartmentDao
    {
        public Department GetByName(string name)
        {
            var query = CreateQuery("from Department where Name = :name").SetString("name", name);
            return query.UniqueResult<Department>();
        }

        public Page<Department> Search(DepartmentSearchForm searchForm)
        {
            var query = QueryOver();

            query = query.OrderBy(e => e.Ordinal).Desc;

            return GetPage(query, searchForm);
        }
    }
}
