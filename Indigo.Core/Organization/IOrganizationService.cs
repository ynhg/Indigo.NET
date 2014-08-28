using Indigo.Infrastructure.Search;
using Indigo.Organization.Search;
using Indigo.Security;
using System.Collections.Generic;

namespace Indigo.Organization
{
    public interface IOrganizationService
    {
        Department AddDepartment(string name, string description, Department superDepartment, int ordinal, User oper);
        Department GetDepartmentById(string id);
        Department GetDepartmentById(string id, User oper);
        Department GetDepartmentByName(string name);
        IList<Department> GetDepartments();
        IList<Department> GetDepartments(User oper);
        Page<Department> Search(DepartmentSearchForm searchForm);

        Employee AddEmployee(Employee employee, Department department, Position position);
        Page<Employee> Search(EmployeeSearchForm searchForm);
    }
}
