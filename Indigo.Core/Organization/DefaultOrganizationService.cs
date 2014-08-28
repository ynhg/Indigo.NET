using Indigo.Infrastructure.Search;
using Indigo.Organization.Data;
using Indigo.Organization.Search;
using Indigo.Security;
using Spring.Objects.Factory.Attributes;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;
using System.Collections.Generic;

namespace Indigo.Organization
{
    [Service]
    public class DefaultOrganizationService : IOrganizationService
    {
        [Transaction]
        public Department AddDepartment(string name, string description, Department superDepartment, int ordinal, User oper)
        {
            Department newDepartment = new Department();
            newDepartment.Name = name;
            newDepartment.Description = description;
            newDepartment.SuperDepartment = superDepartment;
            newDepartment.Ordinal = ordinal;

            DepartmentDao.Save(newDepartment, oper);

            return newDepartment;
        }

        [Transaction(ReadOnly = true)]
        public Department GetDepartmentById(string id)
        {
            return DepartmentDao.GetById(id);
        }

        [Transaction(ReadOnly = true)]
        public Department GetDepartmentById(string id, User oper)
        {
            return DepartmentDao.GetById(id, oper);
        }

        [Transaction(ReadOnly = true)]
        public Department GetDepartmentByName(string name)
        {
            return DepartmentDao.GetByName(name);
        }

        [Transaction(ReadOnly = true)]
        public IList<Department> GetDepartments()
        {
            return DepartmentDao.FindAll();
        }

        [Transaction(ReadOnly = true)]
        public IList<Department> GetDepartments(User oper)
        {
            return DepartmentDao.FindAll(oper);
        }

        [Transaction(ReadOnly = true)]
        public Page<Department> Search(DepartmentSearchForm searchForm)
        {
            return DepartmentDao.Search(searchForm);
        }

        [Transaction]
        public Employee AddEmployee(Employee employee, Department department, Position position)
        {
            employee.Department = department;
            employee.Position = position;

            EmployeeDao.Save(employee);

            return employee;
        }

        [Transaction(ReadOnly = true)]
        public Page<Employee> Search(EmployeeSearchForm searchForm)
        {
            return EmployeeDao.Search(searchForm);
        }

        [Autowired]
        public IDepartmentDao DepartmentDao { get; set; }

        [Autowired]
        public IEmployeeDao EmployeeDao { get; set; }
    }
}
