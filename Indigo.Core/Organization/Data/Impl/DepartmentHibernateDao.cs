using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data.Impl;
using NHibernate;
using Spring.Stereotype;

namespace Indigo.Organization.Data.Impl
{
    [Repository]
    public class DepartmentHibernateDao : GenericSecurityHibernateDao<Department, string>, IDepartmentDao
    {
        public Department GetByName(string name)
        {
            return QueryOver().Where(d => d.Name == name).SingleOrDefault();
        }

        public IList<Department> FindAll(string superDepartmentId)
        {
            return QueryOver().Where(d => d.SuperDepartment.Id == superDepartmentId).List();
        }

        public Page<Department> Search(DepartmentSearchForm searchForm)
        {
            IQueryOver<Department, Department> query = QueryOver();

            query = query.OrderBy(e => e.Ordinal).Desc;

            return GetPage(query, searchForm);
        }
    }
}