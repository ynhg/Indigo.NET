using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security.Data;

namespace Indigo.Organization.Data
{
    public interface IEmployeeDao : IGenericSecurityDao<Employee, string>
    {
        Employee GetByName(string name);
        Page<Employee> Search(EmployeeSearchForm searchForm);
    }
}
