using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data.Impl;
using Spring.Stereotype;

namespace Indigo.Organization.Data.Impl
{
    [Repository]
    public class EmployeeHibernateDao : GenericSecurityHibernateDao<Employee, string>, IEmployeeDao
    {
        public Employee GetByName(string name)
        {
            var query = CreateQuery("from Employee where Name = :name").SetString("name", name);
            return query.UniqueResult<Employee>();
        }

        public Page<Employee> Search(EmployeeSearchForm searchForm)
        {
            var query = QueryOver();

            query = query.OrderBy(e => e.Created).Desc;

            return GetPage(query, searchForm);
        }
    }
}
