using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data;

namespace Indigo.Organization.Data
{
    public interface IDepartmentDao : IGenericSecurityDao<Department, string>
    {
        Department GetByName(string name);
        IList<Department> FindAll(string superDepartmentId);
        Page<Department> Search(DepartmentSearchForm searchForm);
    }
}