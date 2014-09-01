using System.Collections.Generic;
using Indigo.Infrastructure.Search;
using Indigo.Organization.Data;
using Indigo.Organization.Search;
using Indigo.Security;
using Spring.Objects.Factory.Attributes;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;

namespace Indigo.Organization
{
    [Service]
    public class DefaultOrganizationService : IOrganizationService
    {
        [Autowired]
        public IDepartmentDao DepartmentDao { get; set; }

        [Autowired]
        public IPositionDao PositionDao { get; set; }

        [Autowired]
        public IEmployeeDao EmployeeDao { get; set; }

        [Transaction]
        public Department AddDepartment(string name, string description, Department superDepartment, int ordinal,
            User oper)
        {
            var newDepartment = new Department
            {
                Name = name,
                Description = description,
                SuperDepartment = superDepartment,
                Ordinal = ordinal
            };

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
        public IList<Department> GetRootDepartments()
        {
            return DepartmentDao.FindAll(null);
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
        public Position AddPosition(string name, int rank, User oper)
        {
            var position = new Position(name, rank);

            PositionDao.Save(position, oper);

            return position;
        }

        [Transaction(ReadOnly = true)]
        public Position GetPositionById(string id)
        {
            return PositionDao.GetById(id);
        }

        [Transaction(ReadOnly = true)]
        public Position GetPositionByName(string name)
        {
            return PositionDao.GetByName(name);
        }

        [Transaction(ReadOnly = true)]
        public IList<Position> GetPositions()
        {
            return PositionDao.FindAll();
        }

        [Transaction(ReadOnly = true)]
        public Page<Position> Search(PositionSearchForm searchForm)
        {
            return PositionDao.Search(searchForm);
        }

        [Transaction]
        public Employee AddEmployee(Employee employee, Department department, Position position, User oper)
        {
            employee.Department = department;
            employee.Position = position;

            EmployeeDao.Save(employee, oper);

            return employee;
        }

        [Transaction(ReadOnly = true)]
        public Employee GetEmployeeByNumber(string number)
        {
            return EmployeeDao.GetByNumber(number);
        }

        [Transaction(ReadOnly = true)]
        public Page<Employee> Search(EmployeeSearchForm searchForm)
        {
            return EmployeeDao.Search(searchForm);
        }
    }
}